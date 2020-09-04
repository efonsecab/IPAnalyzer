using DnsClient.Internal;
using IPAnalyzer.Services.Configuration;
using IPAnalyzer.Services.Interfaces;
using IPAnalyzer.Services.Models.GeoLocation;
using IPAnalyzer.Services.Models.GetCurrentWeather;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IPAnalyzer.Services
{
    public class WeatherService: IWeatherService
    {
        private AzureMapsConfiguration AzureMapsConfiguration { get; }
        private HttpClient HttpClient { get; }
        private ILogger<WeatherService> Logger { get; }

        public WeatherService(AzureMapsConfiguration azureMapsConfiguration, HttpClient httpClient, 
            ILogger<WeatherService> logger)
        {
            this.AzureMapsConfiguration = azureMapsConfiguration;
            this.HttpClient = httpClient;
            this.Logger = logger;
        }

        public async Task<GetCurrentWeatherResponse> GetCurrentWeatherAsync(GeoCoordinates geoCoordinates, 
            CancellationToken cancellationToken=default)
        {
            try
            {
                string query = $"{geoCoordinates.Latitude},{geoCoordinates.Longitude}";
                string requestUrl = $"https://atlas.microsoft.com/weather/currentConditions/json" +
                    $"?subscription-key={this.AzureMapsConfiguration.Key}" +
                    $"&api-version=1.0" +
                    $"&query={query}" +
                    $"&details={true}" +
                    $"&duration={0}";
                var response = await this.HttpClient.GetFromJsonAsync<GetCurrentWeatherResponse>(requestUrl, cancellationToken);
                return response;
            }
            catch ( Exception ex)
            {
                this.Logger?.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
