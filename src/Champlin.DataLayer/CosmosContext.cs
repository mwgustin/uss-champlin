using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Options;

namespace Champlin.DataLayer
{
    public class CosmosContext
    {
        public CosmosClient Client;
        private readonly IOptions<CosmosContextSettings> _contextSettings;

        public CosmosContext(IOptions<CosmosContextSettings> contextSettings)
        {
            _contextSettings = contextSettings;
            BuildClient();
        }

        public Container GetContainer(string DbName, string CollectionName)
        {
            return Client.GetContainer(DbName, CollectionName);
        }

        public void BuildClient()
        {
            var builder = new CosmosClientBuilder(_contextSettings.Value.ConnectionString);
            builder.WithApplicationName(_contextSettings.Value.ApplicationName);
            builder.AddCustomHandlers(_contextSettings.Value.Handlers);

            Client = builder.Build();
        }
    }
}