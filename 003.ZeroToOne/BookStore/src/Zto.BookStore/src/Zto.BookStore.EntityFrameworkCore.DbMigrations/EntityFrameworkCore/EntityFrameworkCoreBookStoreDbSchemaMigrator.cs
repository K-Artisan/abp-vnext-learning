using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Zto.BookStore.Data;

namespace Zto.BookStore.EntityFrameworkCore
{
    public class EntityFrameworkCoreBookStoreDbSchemaMigrator : IBookStoreDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreBookStoreDbSchemaMigrator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /*
            * 我们有意从IServiceProvider解析BookStoreMigrationsDbContext(而不是直接注入它)，
            * 是为了能正确获取当前的范围、当前租户的连接字符串
            */
            var dbContext = _serviceProvider.GetRequiredService<BookStoreMigrationsDbContext>();
            var database = dbContext.Database;
            //var connString = database.GetConnectionString();

            /*
             * Asynchronously applies any pending migrations for the context to the database.
             * Will create the database if it does not already exist.
             */
            await database.MigrateAsync();
        }
    }
}
