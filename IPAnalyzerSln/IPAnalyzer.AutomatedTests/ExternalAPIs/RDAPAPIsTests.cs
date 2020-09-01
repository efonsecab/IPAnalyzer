using System;
using System.Net.Http;
using System.Threading.Tasks;
using IPAnalyzer.AutomatedTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IPAnalyzer.AutomatedTests.ExternalAPIs
{
    [TestClass]
    public class RDAPAPIsTests
    {
        [TestMethod]
        public async Task Test_GetRDAPInfoByDomainAsync()
        {
            string domain = Constants.TEST_HOSTNAME;
            string requestUrl = $"https://rdap.verisign.com/com/v1/domain/{domain}";
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync(requestUrl);
            Assert.IsTrue(response.IsSuccessStatusCode,
                $"Unable to retrieve infomation: {response.ReasonPhrase}");
        }

        [TestMethod]
        public async Task Test_GetRDAPInfoByIP()
        {
            string ipv4Address = Constants.TEST_IP_V4;
            string baseUrl = $"https://rdap.lacnic.net/rdap/ip/";
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"{baseUrl}{ipv4Address}");
            Assert.IsTrue(response.IsSuccessStatusCode,
                $"Unable to retrieve infomation for {ipv4Address}: {response.ReasonPhrase}");
        }
    }
}
