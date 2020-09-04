using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using IPAnalyzerAPI.Configuration;
using IPAnalyzerAPI.Controllers.v1._0.Models;
using IPAnalyzerAPI.Controllers.v1._0.Models.GetGeoLocation;
using IPAnalyzerAPI.Controllers.v1._0.Models.GetWeather;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IPAnalyzerAPI.Controllers.v1._0
{
    [Route("api/{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "WebsitesTools")]
    public class WebsitesToolsController : ControllerBase
    {
        private List<AnalysisService> DefaultAnalysisServices = new List<AnalysisService>()
        {
            AnalysisService.Weather,
            AnalysisService.RDAP,
            AnalysisService.ReverseDNS
        };
        private ServicesConfiguration ServicesConfiguration { get; }
        private HttpClient HttpClient { get; }

        public WebsitesToolsController(ServicesConfiguration servicesConfiguration,
            HttpClient httpClient)
        {
            this.ServicesConfiguration = servicesConfiguration;
            this.HttpClient = httpClient;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Analyze([FromQuery] AnalyzeModel model)
        {
            AnalyzeResponseModel result = new AnalyzeResponseModel();
            var services = (model.RequestedServices ?? DefaultAnalysisServices).Distinct().ToList();
            List<Task> lstTasks = new List<Task>();
            if (IPAddress.TryParse(model.HostnameOrddress, out IPAddress ipAddrees))
            {
                if (services.Contains(AnalysisService.Weather))
                {
                    result.GeoLocation = result.GeoLocation = await this.GetGeoLocationAsync(ipAddrees);
                }
                foreach (var singleService in services)
                {
                    switch (singleService)
                    {
                        case AnalysisService.GeoLocation:
                            lstTasks.Add(this.GetGeoLocationAsync(ipAddrees)
                                .ContinueWith(p =>
                                {
                                    if (p.IsCompletedSuccessfully)
                                        result.GeoLocation = result.GeoLocation ?? p.Result;
                                }));
                            break;
                        case AnalysisService.Weather:
                            lstTasks.Add(this.GetWeatherAsync(latitude: result.GeoLocation.latitude, longitude: result.GeoLocation.longitude)
                                .ContinueWith(p =>
                                {
                                    if (p.IsCompletedSuccessfully)
                                        result.Weather = p.Result;
                                })
                                );
                            break;
                        case AnalysisService.RDAP:
                            lstTasks.Add(this.GetRDap(ipAddrees));
                            break;
                    }
                }
                Task.WaitAll(lstTasks.ToArray());
            }
            return Ok(result);
        }

        private async Task GetRDap(IPAddress ipAddrees)
        {
            string baseApiUrl = this.ServicesConfiguration["GetGeoLocation"].FunctionUrl;
        }

        private async Task<GetGeoLocationResponse> GetGeoLocationAsync(IPAddress ipAddrees)
        {
            string baseApiUrl = this.ServicesConfiguration["GetGeoLocation"].FunctionUrl;
            string requestUrl = $"{baseApiUrl}?ipAddress={ipAddrees.ToString()}";
            var response = await this.HttpClient.GetFromJsonAsync<GetGeoLocationResponse>(requestUrl);
            return response;
        }

        private async Task<Weather[]> GetWeatherAsync(double latitude, double longitude)
        {
            string baseApiUrl = this.ServicesConfiguration["GetWeather"].FunctionUrl;
            string requestUrl = $"{baseApiUrl}?latitude={latitude}&longitude={longitude}";
            var response = await this.HttpClient.GetFromJsonAsync<Weather[]>(requestUrl);
            return response;
        }
    }
}
