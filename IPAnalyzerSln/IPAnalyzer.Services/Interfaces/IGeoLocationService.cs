using IPAnalyzer.Services.Models.GetGeoLocationInfo;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace IPAnalyzer.Services.Interfaces
{
    public interface IGeoLocationService
    {
        Task<GetGeoLocationInfoResponse> GetIpGeoLocationInfo(IPAddress ipAddress, CancellationToken cancellationToken = default);
    }
}