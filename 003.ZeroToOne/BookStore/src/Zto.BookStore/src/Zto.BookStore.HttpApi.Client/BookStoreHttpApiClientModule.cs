using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Zto.BookStore
{
    [DependsOn(
        typeof(BookStoreApplicationContractsModule), //包含应用服务接口
        typeof(AbpHttpClientModule)                  //用来创建客户端代理
    )]
    public class BookStoreHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "BookStore";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            //创建动态客户端代理
            context.Services.AddHttpClientProxies(
                typeof(BookStoreApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
