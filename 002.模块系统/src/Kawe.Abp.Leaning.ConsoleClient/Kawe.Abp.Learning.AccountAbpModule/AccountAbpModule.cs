using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.Modularity;

namespace Kawe.Abp.Learning.AccountAbpModule
{
    public class AccountAbpModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<AccountService>();
        }
    }
}
