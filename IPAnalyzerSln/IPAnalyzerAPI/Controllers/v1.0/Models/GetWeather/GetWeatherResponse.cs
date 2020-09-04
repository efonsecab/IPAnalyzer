using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPAnalyzerAPI.Controllers.v1._0.Models.GetWeather
{
    public class Weather
    {
        public float Temperature { get; internal set; }
        public string TemperatureUnit { get; set; }
    }

}
