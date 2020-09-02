using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPAnalyzer.AutomatedTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PTI.WebsitesTools;

namespace IPAnalyzer.AutomatedTests
{
    [TestClass]
    public class SitemapsScannerTests
    {
        [TestMethod]
        public async Task Test_GetSitemapsContents()
        {
            string websiteUrl = Constants.TEST_SITEMAP_YAHOO_URL;
            SitemapScanner sitemapScanner = new SitemapScanner();
            var lstSitemapsXmls = await sitemapScanner.GetSitemapsContents(url:websiteUrl);
            Assert.IsTrue(lstSitemapsXmls.Count() > 0, "No sitemaps found");
        }

        [TestMethod]
        public async Task Test_GetSitemapsUrls()
        {
            string websiteUrl = Constants.TEST_SITEMAP_YAHOO_URL;
            SitemapScanner sitemapScanner = new SitemapScanner();
            var lstSitemapsXmls = await sitemapScanner.GetSitemapsUrls(url: websiteUrl);
            Assert.IsTrue(lstSitemapsXmls.Count() > 0, "No sitemaps found");
        }

        [TestMethod]
        public async Task Test_GetSitemapDeserializedInfo()
        {
            string sitemapUrl = Constants.TEST_SITEMAP_XML_URL;
            SitemapScanner sitemapScanner = new SitemapScanner();
            SitemapInfo sitemapInfo = await sitemapScanner.GetSitemapInfo(sitemapUrl);
            Assert.IsNotNull(sitemapInfo, "Unable to get sitemap information");
        }
    }
}
