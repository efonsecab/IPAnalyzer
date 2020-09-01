using IPAnalyzer.AutomatedTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IPAnalyzer.AutomatedTests.FrameworkBaseAPIs
{
    [TestClass]
    public class PingTests
    {
        [TestMethod]
        public async Task Test_PingIpAddress()
        {
            int timeoutMilliSeconds = 1000;
            Ping ping = new Ping();
            var pingReply = await ping.SendPingAsync(Constants.TEST_REVERSEDNS_HOSTNAME, timeout:timeoutMilliSeconds);
            Assert.IsTrue(pingReply.Status == IPStatus.Success);
            pingReply = await ping.SendPingAsync(Constants.TEST_HOSTNAME, timeout: timeoutMilliSeconds);
            Assert.IsTrue(pingReply.Status == IPStatus.TimedOut);
        }
    }
}
