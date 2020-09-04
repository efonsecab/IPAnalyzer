using DnsClient.Internal;
using IPAnalyzer.Services.Interfaces;
using IPAnalyzer.Services.Models.Robots;
using Microsoft.Extensions.Logging;
using PTI.WebsitesTools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace IPAnalyzer.Services
{
    public class RobotsService : IRobotsService
    {
        private ILogger Logger { get; }
        private SitemapScanner SitemapScanner { get; }

        public RobotsService(ILogger logger)
        {
            this.Logger = logger;
            SitemapScanner = new SitemapScanner(logger);
        }

        /// <summary>
        /// Retrieves the sitemaps information for a specified url if there is a robots.txt file
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<RobotsInfo> GetRobotsInfoAsync(string url)
        {
            try
            {
                var sitemapsUrls = await this.SitemapScanner.GetSitemapsUrls(url);
                RobotsInfo robotsInfo = new RobotsInfo()
                {
                    SitemapsUrls = sitemapsUrls
                };
                return robotsInfo;
            }
            catch (Exception ex)
            {
                this.Logger?.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
