using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;


namespace Zto.BookStore.EntityFrameworkCore
{
    /// <summary>
    /// This DbContext is only used for database migrations.
    /// It is not used on runtime. See BookStoreDbContext for the runtime DbContext.
    /// It is a unified model that includes configuration for
    /// all used modules and your application.
    /// 
    /// 这个DbContext只用于数据库迁移。
    /// 它不在运行时使用。有关运行时DbContext，请参阅BookStoreDbContext。
    /// 它是一个统一配置所有使用的模块和您的应用程序的模型
    /// </summary>
    [ConnectionStringName("BookStoreConnString")]
    public class BookStoreMigrationsDbContext : AbpDbContext<BookStoreMigrationsDbContext>
    {
        public BookStoreMigrationsDbContext(DbContextOptions<BookStoreMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */
            // 配置从其它modules引入的模型



            /* Configure your own tables/entities inside the ConfigureBookStore method */
            // 配置本项目自己的表和实体模型
            builder.ConfigureBookStore();
        }

    }
}
