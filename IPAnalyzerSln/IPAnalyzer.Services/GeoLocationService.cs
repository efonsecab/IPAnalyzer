using DnsClient.Internal;
using IPAnalyzer.Services.Configuration;
using IPAnalyzer.Services.Interfaces;
using IPAnalyzer.Services.Models.GetGeoLocationInfo;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IPAnalyzer.Services
{
    public class GeoLocationService: IGeoLocationService
    {
        private ILogger Logger { get; }
        private IpStackConfiguration IpStackConfiguration { get; }
        private HttpClient HttpClient { get; }

        public GeoLocationService(ILogger logger, HttpClient httpClient, IpStackConfiguration ipStackConfiguration)
        {
            this.Logger = logger;
            this.IpStackConfiguration = ipStackConfiguration;
            this.HttpClient = httpClient;
        }

        public async Task<GetGeoLocationInfoResponse> GetIpGeoLocationInfo(IPAddress ipAddress,
            CancellationToken cancellationToken=default)
        {
            string requestUrl =
                $"http://api.ipstack.com/{ipAddress}" +
                $"?access_key={this.IpStackConfiguration.Key}&format=1";
            try
            {
                var result = await HttpClient.GetFromJsonAsync<GetGeoLocationInfoResponse>(requestUrl, cancellationToken);
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
