using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Champlin.DataLayer;
using Champlin.Common;
using System.Linq;

namespace Champlin.API
{
    public class GetCrewItem
    {
        private readonly IRepository<Crew> _crewRepo;

        public GetCrewItem(IRepository<Crew> crewRepo)
        {
            _crewRepo = crewRepo;
        }

        [FunctionName("GetCrewItem")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string origUrl = req.Query["origUrl"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            origUrl = origUrl ?? data?.origUrl;

            log.LogInformation($"Url: {origUrl}");

            var result = await _crewRepo.GetBySpecificationAsync(new GetCrewByUrlSpec(origUrl));

           if( result == null || result.Count == 0)
           {
               return new NotFoundObjectResult($"Could not find url: {origUrl}");
           }

            return new OkObjectResult(result.First());
        }
    }
}
