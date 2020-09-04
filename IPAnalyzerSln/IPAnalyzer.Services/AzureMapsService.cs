using DnsClient.Internal;
using IPAnalyzer.Services.Configuration;
using IPAnalyzer.Services.Interfaces;
using IPAnalyzer.Services.Models.GeoLocation;
using IPAnalyzer.Services.Models.GetCurrentConditions;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace IPAnalyzer.Services
{
    public class AzureMapsService: IAzureMapsService
    {
        private ILogger<AzureMapsService> Logger { get; }
        private AzureMapsConfiguration AzureMapsConfiguration { get; }
        private HttpClient HttpClient { get; }

        public AzureMapsService(ILogger<AzureMapsService> logger, AzureMapsConfiguration azureMapsConfiguration,
            HttpClient httpClient)
        {
            this.Logger = logger;
            this.AzureMapsConfiguration = azureMapsConfiguration;
            this.HttpClient = httpClient;
        }

        #region Weather
        public async Task<GetCurrentConditionsResponse> GetCurrentConditionsAsync(GeoCoordinates geoCoordinates,
            CancellationToken cancellationToken = default)
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
                var response = await this.HttpClient.GetFromJsonAsync<GetCurrentConditionsResponse>(requestUrl, cancellationToken);
                return response;
            }
            catch (Exception ex)
            {
                this.Logger?.LogError(ex, ex.Message);
                throw;
            }
        }
        #endregion

    }
}
