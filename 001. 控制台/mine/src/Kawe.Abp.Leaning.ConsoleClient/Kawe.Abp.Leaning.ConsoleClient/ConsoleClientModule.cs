using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Modularity;

namespace Kawe.Abp.Leaning.ConsoleClient
{
    public class ConsoleClientModule : AbpModule
    {
       public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //在此处注入依赖项
            context.Services.AddSingleton<IMyService,MyService>();
        }
    }
}
