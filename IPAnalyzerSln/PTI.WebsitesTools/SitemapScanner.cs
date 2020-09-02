using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Extensions.Logging;
using PTI.WebsitesTools.CustomExceptions;

namespace PTI.WebsitesTools
{
    public class SitemapScanner
    {
        private ILogger<SitemapScanner> Logger { get; }

        public SitemapScanner(ILogger<SitemapScanner> logger=null)
        {
            this.Logger = logger;
        }

        /// <summary>
        /// Retrieves a list of sitemaps urls found in the specified url
        /// </summary>
        /// <param name="url"></param>
        /// <exception cref="RobotsFileNotFoundException"></exception>
        /// <exception cref="Exception"></exception>
        /// <returns></returns>
        public async Task<List<string>> GetSitemapsUrls(string url)
        {
            string robotsUrl = $"{url}/robots.txt";
            try
            {
                System.Net.Http.HttpClient httpClient = new HttpClient();
                var robotsFileText = await httpClient.GetStringAsync(robotsUrl);
                string currentTextLine = string.Empty;
                List<string> lstSitemapsUrls = new List<string>();
                using (System.IO.StringReader textReader = new System.IO.StringReader(robotsFileText))
                {
                    while ((currentTextLine = textReader.ReadLine()) != null)
                    {
                        if (currentTextLine.ToLower().StartsWith("sitemap:"))
                        {
                            //Check robots.txt specifications, spaces are optional
                            //https://developers.google.com/search/reference/robots_txt?hl=en
                            var sitemapUrl = currentTextLine.Substring(8).TrimStart();
                            lstSitemapsUrls.Add(sitemapUrl);
                        }
                    }
                }
                return lstSitemapsUrls;
            }
            catch(InvalidOperationException requestException)
            {
                string message = $"Unable to retrieve " +
                    $"robots file at: {robotsUrl}";
                this.LogException(requestException, message);
                throw new RobotsFileNotFoundException(message,requestException);
            }
            catch (Exception ex)
            {
                this.LogException(ex);
                throw;
            }
        }

        private void LogException(Exception ex, string message=null)
        {
            if (this.Logger != null)
            {
                string detailedMessage = $"An error occured. Unable to perform request." +
                    "Check your log for more details";
                if (message != null)
                    detailedMessage += $": {message}";
                this.Logger.LogError(ex, detailedMessage);
            }
        }

        /// <summary>
        /// Returns the contents for sitemaps found in the specified url.
        /// This method looks for a robots.txt file
        /// </summary>
        /// <param name="url">Base url where to look for sitemaps</param>
        /// <returns></returns>
        public async Task<List<string>> GetSitemapsContents(string url)
        {
            string robotsUrl = $"{url}/robots.txt";
            HttpClient httpClient = new HttpClient();
            var robotsFileText = await httpClient.GetStringAsync(robotsUrl);
            string currentTextLine = string.Empty;
            List<string> lstSitemapsXmls = new List<string>();
            using (System.IO.StringReader textReader = new System.IO.StringReader(robotsFileText))
            {
                while ((currentTextLine = textReader.ReadLine()) != null)
                {
                    if (currentTextLine.ToLower().StartsWith("sitemap:"))
                    {
                        //Check robots.txt specifications, spaces are optional
                        //https://developers.google.com/search/reference/robots_txt?hl=en
                        var sitemapUrl = currentTextLine.Substring(8).TrimStart();
                        byte[] sitemapBytes = null;
                        switch (System.IO.Path.GetExtension(sitemapUrl))
                        {
                            case ".gz":
                                sitemapBytes = await httpClient.GetByteArrayAsync(sitemapUrl);
                                using (MemoryStream sourceStream = new MemoryStream(sitemapBytes))
                                {
                                    using (MemoryStream destStream = new MemoryStream())
                                    {
                                        using (GZipStream gZipStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                                        {
                                            await gZipStream.CopyToAsync(destStream);
                                        }
                                        //Check specification https://developers.google.com/search/reference/robots_txt?hl=en#file-format
                                        string sitemapUtf8String = Encoding.UTF8.GetString(destStream.ToArray());
                                        lstSitemapsXmls.Add(sitemapUtf8String);
                                    }
                                }
                                break;
                            case ".xml":
                                string sitemapXml = await httpClient.GetStringAsync(sitemapUrl);
                                lstSitemapsXmls.Add(sitemapXml);
                                break;
                        }
                    }
                }
            }
            return lstSitemapsXmls;
        }

        /// <summary>
        /// Retrieves the data from the sitemap in the specified url
        /// </summary>
        /// <param name="sitemapXmlUrl">Url of the sitemap xml file.</param>
        /// <returns></returns>
        public async Task<SitemapInfo> GetSitemapInfo(string sitemapXmlUrl)
        {
            SitemapInfo result = null;
            HttpClient httpClient = new HttpClient();
            var sitemapContents = await httpClient.GetStringAsync(sitemapXmlUrl);
            XmlSerializer serializer = new XmlSerializer(typeof(SitemapInfo));
            using (TextReader reader = new StringReader(sitemapContents))
            {
                result = serializer.Deserialize(reader) as SitemapInfo;
            }
            return result;
        }
    }
}
