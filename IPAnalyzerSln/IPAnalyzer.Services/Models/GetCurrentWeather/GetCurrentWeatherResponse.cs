using System;
using System.Collections.Generic;
using System.Text;

namespace IPAnalyzer.Services.Models.GetCurrentWeather
{
    public class GetCurrentWeatherResponse
    {
        public float Temperature { get; internal set; }
        public string TemperatureUnit { get; internal set; }
    }
}
