using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using PTI.AzureFunctions.WebsitesTools.Models;
using PTI.WebsitesTools;

namespace PTI.AzureFunctions.WebsitesTools
{
    public static class GetSitemapsInfo
    {
        [FunctionName("GetSitemapsInfo")]
        public static async Task<List<GetSitemapInfoResultModel>> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var outputs = new List<GetSitemapInfoResultModel>();
            var inputData = context.GetInput<GetSitemapsInfoModel>();
            // Replace "hello" with the name of your Durable Activity Function.
            foreach (var singleUrl in inputData.WebsitesUrls)
            {
                outputs.Add(await context.CallActivityAsync<GetSitemapInfoResultModel>("GetSitemapsInfo_Process", singleUrl));
            }

            // returns ["Hello Tokyo!", "Hello Seattle!", "Hello London!"]
            return outputs;
        }

        [FunctionName("GetSitemapsInfo_Process")]
        public static async Task<GetSitemapInfoResultModel> ProcessAsync([ActivityTrigger] string url, ILogger log)
        {
            GetSitemapInfoResultModel result = null;
            SitemapScanner sitemapScanner = new PTI.WebsitesTools.SitemapScanner(log);
            var getsitgetSitemapsUrlResult = await sitemapScanner.GetSitemapsUrls(url);
            result = new GetSitemapInfoResultModel()
            {
                Url = url,
                SitemapsUrls = getsitgetSitemapsUrlResult
            };
            return result;
        }

        [FunctionName("GetSitemapsInfo_HttpStart")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            var inputData = await req.Content.ReadAsAsync<GetSitemapsInfoModel>();
            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync<GetSitemapsInfoModel>("GetSitemapsInfo", inputData);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}