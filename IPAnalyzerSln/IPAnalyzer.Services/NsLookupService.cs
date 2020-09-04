using DnsClient;
using DnsClient.Internal;
using DnsClient.Protocol;
using IPAnalyzer.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IPAnalyzer.Services
{
    public class NsLookupService : INsLookupService
    {
        private ILogger<NsLookupService> Logger { get; }
        private ILookupClient LookupClient { get; }

        public NsLookupService(ILogger<NsLookupService> logger, ILookupClient lookupClient)
        {
            this.Logger = logger;
            this.LookupClient = lookupClient;
        }

        public async Task<List<MxRecord>> GetMXRecordsAsync(string domainName, CancellationToken cancellationToken=default)
        {
            try
            {
                var response = await this.LookupClient.QueryAsync(domainName, QueryType.MX, cancellationToken: cancellationToken);
                var mxRecords = response.Answers.MxRecords().ToList();
                return mxRecords;
            }
            catch (Exception ex)
            {
                this.Logger?.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
