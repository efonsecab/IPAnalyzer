using System;
using System.Net;
using System.Threading.Tasks;
using DnsClient;
using IPAnalyzer.AutomatedTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IPAnalyzer.AutomatedTests.ExternalPackages
{
    [TestClass]
    public class DnsClientTests
    {
        [TestMethod()]
        public async Task Test_QueryAny()
        {
            LookupClient lookupClient = new LookupClient();
            var result = await lookupClient.QueryAsync(query: Constants.TEST_HOSTNAME, queryType: QueryType.ANY);
            Assert.IsNotNull(result, $"No data found for {Constants.TEST_HOSTNAME}");
            Assert.IsFalse(result.HasError, $"Error found: {result.ErrorMessage}");
        }

        [TestMethod]
        public async Task Test_ReverseDNS()
        {
            LookupClient lookupClient = new LookupClient();
            var hostEntry = lookupClient.GetHostEntry(Constants.TEST_REVERSEDNS_HOSTNAME);
            var result = await lookupClient.QueryReverseAsync(hostEntry.AddressList[0], 
                queryOptions: new DnsQueryAndServerOptions() {
                    RequestDnsSecRecords=true,
                    Timeout = TimeSpan.FromMinutes(1)
                });
            Assert.IsNotNull(result, $"No data found for : {Constants.TEST_IP_V4}");
            Assert.IsFalse(result.HasError, $"Error found  {result.ErrorMessage}");
        }
    }
}
