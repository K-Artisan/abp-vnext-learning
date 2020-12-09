using Volo.Abp.AspNetCore.Mvc;
using Zto.BookStore.Localization;

namespace Zto.BookStore.Controllers
{
    /* Inherit your controllers from this class.
    */
    public abstract class BookStoreController : AbpController
    {
        protected BookStoreController()
        {
            LocalizationResource = typeof(BookStoreResource);
        }
    }
}
