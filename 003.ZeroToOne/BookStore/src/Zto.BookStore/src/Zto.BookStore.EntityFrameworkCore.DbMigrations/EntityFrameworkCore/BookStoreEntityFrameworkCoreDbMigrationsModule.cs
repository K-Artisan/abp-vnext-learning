using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Microsoft.Extensions.Configuration;

namespace Zto.BookStore.EntityFrameworkCore
{
    [DependsOn(
        typeof(BookStoreEntityFrameworkCoreModule)
        )]
    public class BookStoreEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public BookStoreEntityFrameworkCoreDbMigrationsModule()
        {
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<BookStoreMigrationsDbContext>();
        }
    }
}
