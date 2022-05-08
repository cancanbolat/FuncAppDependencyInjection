using FuncAppDepInject.Config;
using FuncAppDepInject.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(FuncAppDepInject.Startup))]
namespace FuncAppDepInject
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IMyService, MyService>();

            //Options pattern
            builder.Services.AddOptions<Datas>().Configure<IConfiguration>((datas, conf) =>
            {
                conf.GetSection("Datas").Bind(datas);
            });
        }
    }
}
