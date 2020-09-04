using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IPAnalyzer.Services.Interfaces
{
    public interface IReverseDnsService
    {
        Task<List<string>> ResolveDomainNameAsync(IPAddress ipAddress, CancellationToken cancellationToken = default);
    }
}
