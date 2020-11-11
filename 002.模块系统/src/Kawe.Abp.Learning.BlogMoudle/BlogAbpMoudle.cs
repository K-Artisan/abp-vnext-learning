using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Kawe.Abp.Learning.BlogMoudle
{
    public class BlogAbpMoudle : AbpModule
    {
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            base.OnApplicationInitialization(context);
            Console.WriteLine("BlogAbpMoudle->OnApplicationInitialization");
        }
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var services = context.Services;
            services.AddSingleton<BlogService>();
        }
    }
}
