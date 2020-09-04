using IPAnalyzer.Services.Interfaces;
using IPAnalyzer.Services.Models.GetDomainRdapInfo;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace IPAnalyzer.Services
{
    public class RDapService : IRdapService
    {
        private HttpClient HttpClient { get; }
        public RDapService(HttpClient httpClient)
        {
            this.HttpClient = httpClient;
        }

        public async Task<Models.GetIpRdapInfo.GetIpRdapInfoResponse> GetIpRDapInfoAsync(IPAddress ipAddress, CancellationToken cancellationToken = default)
        {
            string requestUrl = $"https://rdap.lacnic.net/rdap/ip/{ipAddress.ToString()}";
            var response = await this.HttpClient.GetFromJsonAsync<Models.GetIpRdapInfo.GetIpRdapInfoResponse>(requestUrl, cancellationToken);
            return response;
        }

        public async Task<Models.GetDomainRdapInfo.GetDomainRdapInfoResponse> GetDomainRdapInfoAsync(string domain, CancellationToken cancellationToken = default)
        {
            string requestUrl = $"https://rdap.verisign.com/com/v1/domain/{domain}";
            var response = await this.HttpClient.GetFromJsonAsync<Models.GetDomainRdapInfo.GetDomainRdapInfoResponse>(requestUrl, cancellationToken);
            return response;
        }
    }
}
