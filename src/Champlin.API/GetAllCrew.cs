using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Champlin.Common;
using Champlin.DataLayer;

namespace Champlin.API
{
    public class GetAllCrew
    {
        private readonly IRepository<Crew> _crewRepo;

        public GetAllCrew(IRepository<Crew> crewRepo)
        {
            _crewRepo = crewRepo;
        }

        [FunctionName("GetAllCrew")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var result = await _crewRepo.GetBySpecificationAsync(new GetAllCrewSpec());
            
            log.LogInformation($"Count of crew: {result.Count}");

            return new OkObjectResult(result);
        }
    }
}
