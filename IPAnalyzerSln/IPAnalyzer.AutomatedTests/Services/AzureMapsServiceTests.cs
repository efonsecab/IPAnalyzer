using IPAnalyzer.Services.Models.GeoLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IPAnalyzer.AutomatedTests.Services
{
    [TestClass]
    public class AzureMapsServiceTests: ServicesTestsBase
    {
        [TestMethod]
        public async Task Test_GetCurrentConditionsAsync()
        {
            var result = await this.AzureMapsService.GetCurrentConditionsAsync(new GeoCoordinates() 
            {
                Latitude  = 9.9983731,
                Longitude = -84.1306463
            });
            Assert.IsNotNull(result);
        }
    }
}
