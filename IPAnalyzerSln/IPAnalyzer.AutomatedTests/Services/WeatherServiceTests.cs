using IPAnalyzer.AutomatedTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPAnalyzer.AutomatedTests.Services
{
    [TestClass]
    public class WeatherServiceTests: ServicesTestsBase
    {
        [TestMethod]
        public async Task Test_GetCurrentWeatherAsync()
        {
            var result = await this.WeatherService.GetCurrentWeatherAsync(Constants.TEST_GEOCOORDINATES);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);
        }
    }
}
