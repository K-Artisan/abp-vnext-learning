using Kawe.Abp.Learning.AccountAbpModule;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Modularity;

namespace Kawe.Abp.Leaning.ConsoleClient
{
    [DependsOn(typeof(AccountAbpModule))]
    public class ConsoleClientModule : AbpModule
    {
       public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //在此处注入依赖项
            context.Services.AddSingleton<IMyService,MyService>();
        }
    }
}
