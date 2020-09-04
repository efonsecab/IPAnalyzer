using DnsClient.Protocol;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IPAnalyzer.Services.Interfaces
{
    public interface INsLookupService
    {
        Task<List<MxRecord>> GetMXRecordsAsync(string domainName, CancellationToken cancellationToken = default);
    }
}
