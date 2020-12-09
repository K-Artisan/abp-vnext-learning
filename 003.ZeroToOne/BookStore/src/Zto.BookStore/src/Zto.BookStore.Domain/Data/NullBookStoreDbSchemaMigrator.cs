using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Zto.BookStore.Data
{
    /// <summary>
    /// 如果数据库提供程序没有定义，则使用
    /// IBookStoreDbSchemaMigrator实现。
    /// </summary>
    public class NullBookStoreDbSchemaMigrator : IBookStoreDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}
