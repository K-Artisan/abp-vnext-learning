using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Zto.BookStore.EntityFrameworkCore
{
    /// <summary>
    ///   This class is needed for EF Core console commands
    ///   (like Add-Migration and Update-Database commands) 
    ///   
    ///   参考资料：
    ///   https://docs.microsoft.com/zh-cn/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli
    ///   从设计时工厂创建DbContext：
    ///   你还可以通过实现接口来告诉工具如何创建 DbContext IDesignTimeDbContextFactory<TContext> ：
    ///   如果实现此接口的类在与派生的项目相同的项目中 DbContext 
    ///   或在应用程序的启动项目中找到，
    ///   则这些工具将绕过创建 DbContext 的其他方法，并改用设计时工厂。
    /// 
    ///   如果需要以不同于运行时的方式配置 DbContext 的设计时，则设计时工厂特别有用 DbContext 。如果构造函数采用其他参数，
    ///   但未在 di 中注册，如果根本不使用 di，
    ///   或者出于某种原因而不是使用 CreateHostBuilder ASP.NET Core 应用程序的类中的方法 Main 。
    /// 
    /// 
    ///   总之一句话：
    ///   实现了IDesignTimeDbContextFactory<BookStoreMigrationsDbContext>，
    ///   就可以使用命令行执行数据库迁移,
    ///      (1).在 NET Core CLI中执行： dotnet ef database update
    ///      (2).在 Visual Studio中执行：Update-Database 
    /// </summary>
    public class BookStoreMigrationsDbContextFactory : IDesignTimeDbContextFactory<BookStoreMigrationsDbContext>
    {
        public BookStoreMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();
            var builder = new DbContextOptionsBuilder<BookStoreMigrationsDbContext>()
                 .UseSqlServer(configuration.GetConnectionString("BookStoreConnString")); //SqlServer数据库
                //.UseMySql(configuration.GetConnectionString("BookStoreConnString"), ServerVersion.); //MySql数据库

            return new BookStoreMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                //项目Zto.BookStore.DbMigrator的根目录
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Zto.BookStore.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
