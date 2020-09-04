using IPAnalyzer.Services.Models.GeoLocation;
using IPAnalyzer.Services.Models.GetCurrentConditions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IPAnalyzer.Services.Interfaces
{
    public interface IAzureMapsService
    {
        Task<GetCurrentConditionsResponse> GetCurrentConditionsAsync(GeoCoordinates geoCoordinates,
            CancellationToken cancellationToken = default);
    }
}
