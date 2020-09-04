using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IPAnalyzer.Services.Interfaces;
using System.Net;

namespace PTI.AzureFunctions.WebsitesTools
{
    public  class GetGeoLocation
    {
        private IGeoLocationService GeoLocationService { get; }
        public GetGeoLocation(IGeoLocationService geoLocationService)
        {
            this.GeoLocationService = geoLocationService;
        }

        [FunctionName("GetGeoLocation")]
        public  async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string ipAddressString = req.Query["ipAddress"];
            if (IPAddress.TryParse(ipAddressString, out IPAddress ipAddress))
            {
                var geoLocation = await this.GeoLocationService.GetIpGeoLocationInfo(ipAddress);
                return new OkObjectResult(geoLocation);
            }
            else
                return new OkObjectResult("Invalid ip Address");
        }
    }
}
