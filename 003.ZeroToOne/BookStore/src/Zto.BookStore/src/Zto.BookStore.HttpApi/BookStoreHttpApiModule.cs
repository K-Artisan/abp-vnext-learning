using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Modularity;

namespace Zto.BookStore
{
    [DependsOn(
        typeof(BookStoreApplicationContractsModule)
        )]
    public class BookStoreHttpApiModule : AbpModule
    {
    }
}
