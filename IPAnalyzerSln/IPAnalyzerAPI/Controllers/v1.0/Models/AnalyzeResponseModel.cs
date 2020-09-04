using IPAnalyzerAPI.Controllers.v1._0.Models.GetWeather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPAnalyzerAPI.Controllers.v1._0.Models
{
    public class AnalyzeResponseModel
    {
        public GetGeoLocation.GetGeoLocationResponse GeoLocation { get; set; }
        public Weather[] Weather { get; set; }
    }
}
