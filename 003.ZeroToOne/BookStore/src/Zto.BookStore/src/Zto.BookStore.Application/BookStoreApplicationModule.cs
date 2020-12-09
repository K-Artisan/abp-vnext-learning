using Volo.Abp.AutoMapper;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Zto.BookStore
{
    [DependsOn(
        typeof(BookStoreDomainModule),
        typeof(BookStoreApplicationContractsModule),
        typeof(AbpLocalizationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class BookStoreApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                //通过扫描程序集的方式搜索`Porfile`类，并添加到AutoMapper配置中
                options.AddMaps<BookStoreApplicationModule>();
            });
        }
    }
}
