using Volo.Abp.Modularity;

namespace Zto.BookStore
{
    [DependsOn(
     typeof(BookStoreDomainSharedModule)
        )]
    public class BookStoreApplicationContractsModule : AbpModule
    {

    }
}
