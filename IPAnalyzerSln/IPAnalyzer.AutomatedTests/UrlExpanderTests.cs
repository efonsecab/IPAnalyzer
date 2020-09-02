using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PTI.WebsitesTools;

namespace IPAnalyzer.AutomatedTests
{
    [TestClass]
    public class UrlExpanderTests
    {
        [TestMethod]
        public async Task Test_ExpandUrl()
        {
            string shortenedUrl = "https://t.co/3CoAgKLapS";
            UrlExpander urlExpander = new UrlExpander();
            var expandedUrl = await urlExpander.ExpandUrl(shortenedUrl);
            Assert.AreNotEqual(shortenedUrl, expandedUrl,
                $"Error expanding url:{shortenedUrl}");
        }
    }
}
