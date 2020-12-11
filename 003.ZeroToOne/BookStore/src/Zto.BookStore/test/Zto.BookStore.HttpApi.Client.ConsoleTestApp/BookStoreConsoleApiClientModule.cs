using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Zto.BookStore.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(BookStoreHttpApiClientModule)
        )]
    public class BookStoreConsoleApiClientModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<AbpHttpClientBuilderOptions>(options =>
            {
                options.ProxyClientBuildActions.Add((remoteServiceName, clientBuilder) =>
                {
                    clientBuilder.AddTransientHttpErrorPolicy(
                        policyBuilder => policyBuilder.WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(Math.Pow(2, i)))
                    );
                });
            });
        }
    }
}
