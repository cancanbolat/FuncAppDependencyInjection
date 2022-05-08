using Autofac;
using AzureFunctions.Autofac.Configuration;
using FuncAppDepInject.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuncAppDepInject.Config
{
    public class DIConfig
    {
        public DIConfig(string functionName)
        {
            DependencyInjection.Initialize(builder => 
                builder.RegisterType<MyService>().As<IMyService>(), functionName
            );
        }
    }
}
