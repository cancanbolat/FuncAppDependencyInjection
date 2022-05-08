using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureFunctions.Autofac;
using FuncAppDepInject.Config;
using FuncAppDepInject.Services;

namespace FuncAppDepInject
{
    [DependencyInjectionConfig(typeof(DIConfig))]
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] 
            HttpRequest req, ILogger log,
            [Inject] IMyService myService)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string diMessage = myService.MyName($"autofac dependency injection. Hello {name}");

            return new OkObjectResult(diMessage);
        }
    }
}
