using DnsClient;
using IPAnalyzer.AutomatedTests.Helpers;
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
            DnsClient.LookupClient lookupClient = new LookupClient();
            var hostEntry = await lookupClient.GetHostEntryAsync(Constants.TEST_REVERSEDNS_HOSTNAME);
            var ipAddress = hostEntry.AddressList.First();
            ReverseDnsService reverseDnsService = new ReverseDnsService(lookupClient, null);
            var response = await reverseDnsService.ResolveDomainNameAsync(ipAddress);
            Assert.IsNotNull(response, $"Error getting domain name for: {ipAddress}");
            ipAddress = IPAddress.Parse("127.0.0.2");
            response = await reverseDnsService.ResolveDomainNameAsync(ipAddress);
            Assert.AreEqual(null, response, $"Invalid result for: {ipAddress}");
        }
    }
}
