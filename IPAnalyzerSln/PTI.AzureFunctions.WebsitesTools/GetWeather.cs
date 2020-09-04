using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using IPAnalyzer.Services.Configuration;
using IPAnalyzer.Services.Interfaces;
using Microsoft.Extensions.Primitives;
using IPAnalyzer.Services.Models.GeoLocation;

namespace PTI.AzureFunctions.WebsitesTools
{
    public class GetWeather
    {
        private IWeatherService WeatherService { get; }
        public GetWeather(IWeatherService weatherService)
        {
            this.WeatherService = weatherService;
        }

        [FunctionName("GetWeather")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (req.Query.TryGetValue("latitude", out StringValues latitude) &&
                req.Query.TryGetValue("longitude", out StringValues longitude)
                )
            {
                if (double.TryParse(latitude, out double dLatitude) &&
                    double.TryParse(longitude, out double dLongitude))
                {
                    GeoCoordinates geoCoordinates = new GeoCoordinates()
                    {
                        Latitude = dLatitude,
                        Longitude = dLongitude
                    };
                    var result = await this.WeatherService.GetCurrentWeatherAsync(geoCoordinates);
                    return new OkObjectResult(result);
                }
            }
            string name = req.Query["name"];
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
