using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPAnalyzerAPI.Controllers.v1._0.Models
{
    public class AnalyzeModel
    {
        public string HostnameOrddress { get; set; }
        public List<AnalysisService> RequestedServices { get; set; }
    }
}
