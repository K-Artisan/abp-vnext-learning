using Abp;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Zto.BookStore.Books;

namespace Zto.BookStore.EntityFrameworkCore
{
    public static class BookStoreDbContextModelCreatingExtensions
    {
        public static void ConfigureBookStore(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */
            builder.Entity<Book>(e =>
            {
                e.ToTable(BookStoreConsts.DbTablePrefix + "Books", BookStoreConsts.DbSchema);
                e.ConfigureByConvention(); //auto configure for the base class props ,优雅的配置和映射继承的属性,应始终对你所有的实体使用它.
                e.Property(p => p.Name).HasMaxLength(BookConsts.MaxNameLength);

            });
        }
    }
}
