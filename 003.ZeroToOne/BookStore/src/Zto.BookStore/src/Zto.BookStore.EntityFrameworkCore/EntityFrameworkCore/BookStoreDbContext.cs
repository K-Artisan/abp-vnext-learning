using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Zto.BookStore.Books;

namespace Zto.BookStore.EntityFrameworkCore
{
    [ConnectionStringName("BookStoreConnString")]
    public class BookStoreDbContext : AbpDbContext<BookStoreDbContext>
    {
        public DbSet<Book> Books { get; set; }

        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
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
