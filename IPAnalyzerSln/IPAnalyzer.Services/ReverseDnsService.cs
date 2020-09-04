using DnsClient;
using IPAnalyzer.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IPAnalyzer.Services
{
    public class ReverseDnsService : IReverseDnsService
    {
        private ILookupClient LookupClient { get; }
        private ILogger<ReverseDnsService> Logger { get; }

        public ReverseDnsService(ILookupClient lookupClient, ILogger<ReverseDnsService> logger)
        {
            this.LookupClient = lookupClient;
            this.Logger = logger;
        }

        /// <summary>
        /// Retrieves a list of domain names for the given Ip Address.
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns>
        /// A list of domain names, or null in case no names found
        /// </returns>
        public async Task<List<string>> ResolveDomainNameAsync(IPAddress ipAddress, CancellationToken cancellationToken = default)
        {
            var response = await this.LookupClient.QueryReverseAsync(ipAddress, cancellationToken);
            if (response.HasError)
                return null;
            else
            {
                List<string> domainNames = response.AllRecords.Select(p => p.DomainName.Value).ToList();
                return domainNames;
            }
        }
    }
}
