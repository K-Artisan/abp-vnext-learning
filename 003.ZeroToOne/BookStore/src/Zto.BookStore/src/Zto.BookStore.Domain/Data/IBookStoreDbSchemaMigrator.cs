using System.Threading.Tasks;

namespace Zto.BookStore.Data
{
    /// <summary>
    /// 数据库迁移接口
    /// </summary>
    public interface IBookStoreDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
