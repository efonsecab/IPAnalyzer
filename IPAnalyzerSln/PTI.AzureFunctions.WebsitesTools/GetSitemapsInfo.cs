using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IPAnalyzer.Services;
using IPAnalyzer.Services.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using PTI.AzureFunctions.WebsitesTools.Models;
using PTI.WebsitesTools;

namespace PTI.AzureFunctions.WebsitesTools
{
    public class GetSitemapsInfo
    {
        private IRobotsService RobotsService { get; }

        public GetSitemapsInfo(IRobotsService robotsService)
        {
            this.RobotsService = robotsService;
        }
        [FunctionName("GetSitemapsInfo")]
        public async Task<List<GetSitemapInfoResultModel>> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var outputs = new List<GetSitemapInfoResultModel>();
            var inputData = context.GetInput<GetSitemapsInfoModel>();
            foreach (var singleUrl in inputData.WebsitesUrls)
            {
                outputs.Add(await context.CallActivityAsync<GetSitemapInfoResultModel>("GetSitemapsInfo_Process", singleUrl));
            }

            return outputs;
        }

        [FunctionName("GetSitemapsInfo_Process")]
        public async Task<GetSitemapInfoResultModel> ProcessAsync([ActivityTrigger] string url, ILogger log)
        {
            GetSitemapInfoResultModel result = null;
            var robotsInfo = await this.RobotsService.GetRobotsInfoAsync(url);
            result = new GetSitemapInfoResultModel()
            {
                Url = url,
                SitemapsUrls = robotsInfo.SitemapsUrls
            };
            return result;
        }

        [FunctionName("GetSitemapsInfo_HttpStart")]
        public async Task<HttpResponseMessage> HttpStart(
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