using Volo.Abp.Application.Services;
using Zto.BookStore.Localization;

namespace Zto.BookStore
{

    /// <summary>
    /// Inherit your application services from this class.
    /// </summary>
    public abstract class BookStoreAppService : ApplicationService
    {
        protected BookStoreAppService()
        {
            LocalizationResource = typeof(BookStoreResource);
        }
    }
}
