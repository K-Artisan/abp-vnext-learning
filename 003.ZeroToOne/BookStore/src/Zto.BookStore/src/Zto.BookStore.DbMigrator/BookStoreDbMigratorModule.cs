using Volo.Abp.Modularity;
using Volo.Abp.Autofac;
using Zto.BookStore.EntityFrameworkCore;

namespace Zto.BookStore.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(BookStoreEntityFrameworkCoreDbMigrationsModule)
        )]
    public class BookStoreDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
           
        }
    }
}
