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
        //[TestMethod]
        //public async Task Test_PingIpAddress()
        //{
        //    int timeoutMilliSeconds = 60000;
        //    Ping ping = new Ping();
        //    var pingReply = await ping.SendPingAsync(Constants.TEST_REVERSEDNS_HOSTNAME, timeout:timeoutMilliSeconds);
        //    //In Azure DevOps this one returns Timeout
        //    Assert.IsTrue(pingReply.Status == IPStatus.Success, $"Error for ping: {Constants.TEST_REVERSEDNS_HOSTNAME}, - Details: {pingReply.Status.ToString()}");
        //    pingReply = await ping.SendPingAsync(Constants.TEST_HOSTNAME, timeout: timeoutMilliSeconds);
        //    Assert.IsTrue(pingReply.Status == IPStatus.TimedOut, $"Error for ping: {Constants.TEST_REVERSEDNS_HOSTNAME}, - Details: {pingReply.Status.ToString()}");
        //}
    }
}
