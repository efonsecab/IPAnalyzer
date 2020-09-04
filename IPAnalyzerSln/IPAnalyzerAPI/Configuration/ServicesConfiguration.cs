using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPAnalyzerAPI.Configuration
{
    public class ServicesConfiguration
    {
        public List<AzureFunctionsConfiguration> AzureFunctionsConfiguration { get; set; }
        public AzureFunctionsConfiguration this[string index]
        {
            get
            {
                return this.AzureFunctionsConfiguration.Where(p => p.FunctionName == index)
                    .FirstOrDefault();
            }
        }
    }

    public class AzureFunctionsConfiguration
    {
        public string FunctionName { get; set; }
        public string FunctionUrl { get; set; }
    }
}
