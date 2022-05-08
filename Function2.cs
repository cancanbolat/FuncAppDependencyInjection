using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FuncAppDepInject.Services;
using Microsoft.Extensions.Options;
using FuncAppDepInject.Config;

namespace FuncAppDepInject
{
    public class Function2
    {
        private readonly IMyService myService;
        readonly Datas datas;

        public Function2(IMyService myService, IOptions<Datas> dataOptions)
        {
            this.myService = myService;
            this.datas = dataOptions.Value;
        }

        [FunctionName("Function2")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = myService.MyName($"startup dependency injection. Hello {name}. " +
                $"| options pattern değeri => {datas.MyData}" +
                $"| local.settings.jsondan değer okuma => {Environment.GetEnvironmentVariable("AzureWebJobsStorage")}");

            return new OkObjectResult(responseMessage);
        }
    }
}
