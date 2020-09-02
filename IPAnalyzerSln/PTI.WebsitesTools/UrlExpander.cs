using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace PTI.WebsitesTools
{
    public class UrlExpander
    {
        private ILogger Logger { get; }

        public UrlExpander(ILogger logger = null)
        {
            this.Logger = logger;
        }

        /// <summary>
        /// Received a shortened url and returns the expanded url
        /// </summary>
        /// <param name="shortenedUrl"></param>
        /// <exception cref="InvalidProgramException">asdasdad</exception>
        /// <returns></returns>
        public async Task<string> ExpandUrl(string shortenedUrl)
        {
            try
            {
                System.Net.Http.HttpClient httpClient =
                    new System.Net.Http.HttpClient();
                var response = await httpClient.GetAsync(shortenedUrl);
                var expandedUrl = response.RequestMessage.RequestUri.ToString();
                return expandedUrl;
            }
            catch (Exception ex)
            {
                if (this.Logger != null)
                {
                    this.Logger.LogError(ex,
                        $"Unable to expand url: {shortenedUrl}. check your logs for more information");
                }
                throw;
            }
        }
    }
}
