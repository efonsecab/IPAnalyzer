using System;
using System.Collections.Generic;
using System.Text;

namespace PTI.AzureFunctions.WebsitesTools.Models
{
    public class GetSitemapInfoResultModel
    {
        public string Url { get; set; }
        public List<string> SitemapsUrls { get; set; }
    }
}
