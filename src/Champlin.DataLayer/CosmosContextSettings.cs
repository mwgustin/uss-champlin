using Microsoft.Azure.Cosmos;

namespace Champlin.DataLayer
{
    public class CosmosContextSettings
    {
        public string ConnectionString {get; set;}
        public string ApplicationName { get; set; }
        public RequestHandler[] Handlers {get; set;}
    }
}