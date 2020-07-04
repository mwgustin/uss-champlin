using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Champlin.Common
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetBySpecificationAsync(ISpecification<T> spec);
        Task<List<T>> GetByRawQueryAsync(string query);
        Task<T> GetItemByIdAsync(string id, string partitionKey);
        Task<T> UpserItemAsync(T item);
    }
}