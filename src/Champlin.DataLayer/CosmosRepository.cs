using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification;
using Champlin.Common;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Options;

namespace Champlin.DataLayer
{
    public class CosmosRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly Container _container;
        private readonly CosmosContext _context;
        private readonly IOptions<CosmosRepositorySettings> _repoSettings;

        public CosmosRepository(CosmosContext context, IOptions<CosmosRepositorySettings> repoSettings)
        {
            _context = context;
            _repoSettings = repoSettings;
            _container = _context.GetContainer(_repoSettings.Value.DbName, _repoSettings.Value.ContainerName);
        }

        public async Task<List<T>> GetByRawQueryAsync(string query)
        {
            var iterator = _container.GetItemQueryIterator<T>(query);
            return await IterateItemsAsync(iterator);

        }

        public async Task<List<T>> GetBySpecificationAsync(ISpecification<T> spec)
        {
            var queryable = await ApplySpecification(spec);
            var iterator = queryable.ToFeedIterator();
            return await IterateItemsAsync(iterator);
        }

        public async Task<T> GetItemByIdAsync(string id, string partitionKey)
        {
            var result = await _container.ReadItemAsync<T>(id, new PartitionKey(partitionKey));
            return result.Resource;
        }

        public async Task<T> UpserItemAsync(T item)
        {
            var result = await _container.UpsertItemAsync<T>(item);
            return result.Resource;
        }

        private async Task<IQueryable<T>> ApplySpecification(ISpecification<T> specification)
        {
            var queryable = _container.GetItemLinqQueryable<T>();
            return await SpecificationEvaluator<T>.GetQuery(queryable, specification);

        }

        private async Task<List<T>> IterateItemsAsync(FeedIterator<T> iterator)
        {
            List<T> resultList = new List<T>();
            while(iterator.HasMoreResults)
            {
                var queryResult = await iterator.ReadNextAsync();
                resultList.AddRange(queryResult.Resource);
            }
            return resultList;
        }
    }
}
