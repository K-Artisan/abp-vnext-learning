using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace Zto.BookStore
{
    [DependsOn(
        typeof(AbpTenantManagementDomainSharedModule)
    )]
    public class BookStoreDomainSharedModule : AbpModule
    {
    }
}
