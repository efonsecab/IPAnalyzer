using DnsClient;
using IPAnalyzer.Services;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IPAnalyzer.AutomatedTests.Services
{
    [TestClass]
    public class ReverseDnsServiceTests
    {
        [TestMethod()]
        public async Task Test_ResolveDomainName()
        {
            ReverseDnsService reverseDnsService = new ReverseDnsService(new LookupClient(), null);
            var ipAddress = IPAddress.Parse("127.0.0.2");
            var response = await reverseDnsService.ResolveDomainNameAsync(ipAddress);
            Assert.IsTrue(response.Count() > 0, $"Error retrieving domain names for : {ipAddress}");
            ipAddress = IPAddress.Parse("1.1.1.1");
            response = await reverseDnsService.ResolveDomainNameAsync(ipAddress);
        }
    }
}
