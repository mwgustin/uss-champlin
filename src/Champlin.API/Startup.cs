using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Champlin.Common;
using Champlin.DataLayer;
using Microsoft.Extensions.Configuration;
using System.IO;

[assembly: FunctionsStartup(typeof(Champlin.API.Startup))]

namespace Champlin.API
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var Configuration = new ConfigurationBuilder()
                   //.SetBasePath(Environment.CurrentDirectory)  ← do not use
                //    .SetBasePath(Directory.GetCurrentDirectory())
                //    .AddJsonFile("local.settings.json", false)
                   .AddEnvironmentVariables()
                   .Build();   
            
            builder.Services.AddOptions<CosmosContextSettings>().Configure(opts => {
                opts.ConnectionString = Configuration["CosmosConnectionString"];
                opts.ApplicationName = "Champlin Crew API";
            });
            builder.Services.AddOptions<CosmosRepositorySettings>().Configure(opts => {
                opts.DbName = "champlin";
                opts.ContainerName = "champlin";
            });

            builder.Services.AddSingleton<CosmosContext>();
            builder.Services.AddTransient<IRepository<Crew>, CosmosRepository<Crew>>();
        }
    }
}