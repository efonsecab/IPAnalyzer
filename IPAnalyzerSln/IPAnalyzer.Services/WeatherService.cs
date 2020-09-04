using DnsClient.Internal;
using IPAnalyzer.Services.Configuration;
using IPAnalyzer.Services.Interfaces;
using IPAnalyzer.Services.Models.GeoLocation;
using IPAnalyzer.Services.Models.GetCurrentWeather;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IPAnalyzer.Services
{
    public class WeatherService: IWeatherService
    {
        private IAzureMapsService AzureMapsService { get; }
        private HttpClient HttpClient { get; }
        private ILogger<WeatherService> Logger { get; }

        public WeatherService(IAzureMapsService azureMapsService, ILogger<WeatherService> logger)
        {
            this.AzureMapsService = azureMapsService;
            this.Logger = logger;
        }

        public async Task<List<GetCurrentWeatherResponse>> GetCurrentWeatherAsync(GeoCoordinates geoCoordinates, 
            CancellationToken cancellationToken=default)
        {
            try
            {
                var getCurrentConditionsResponse = await this.AzureMapsService
                    .GetCurrentConditionsAsync(geoCoordinates, cancellationToken);
                var result = getCurrentConditionsResponse.results.Select(p => new GetCurrentWeatherResponse()
                {
                    Temperature = p.temperature.value,
                    TemperatureUnit = p.temperature.unit,
                }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                this.Logger?.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
