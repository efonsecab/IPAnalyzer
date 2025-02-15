﻿using IPAnalyzer.AutomatedTests.Helpers;
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
            var result = await this.AzureMapsService.GetCurrentConditionsAsync(Constants.TEST_GEOCOORDINATES);
            Assert.IsNotNull(result);
        }
    }
}
