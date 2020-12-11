using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Zto.BookStore.IdentityServer;

namespace Zto.BookStore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<BookStoreIdentityServerModule>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.InitializeApplication();
        }
    }
}
