using IPAnalyzer.Services.Models.GeoLocation;
using IPAnalyzer.Services.Models.GetCurrentWeather;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IPAnalyzer.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<GetCurrentWeatherResponse> GetCurrentWeatherAsync(GeoCoordinates geoCoordinates, 
            CancellationToken cancellationToken=default);
    }
}
