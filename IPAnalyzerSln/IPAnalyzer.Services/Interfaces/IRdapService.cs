using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IPAnalyzer.Services.Interfaces
{
    public interface IRdapService
    {
        Task<Models.GetIpRdapInfo.GetIpRdapInfoResponse> GetIpRDapInfoAsync(IPAddress ipAddress, CancellationToken cancellationToken=default);
        Task<Models.GetDomainRdapInfo.GetDomainRdapInfoResponse> GetDomainRdapInfoAsync(string domain, CancellationToken cancellationToken = default);
    }
}
