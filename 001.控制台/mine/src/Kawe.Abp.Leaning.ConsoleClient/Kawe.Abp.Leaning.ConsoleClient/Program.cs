using System;
using Volo.Abp;
using Microsoft.Extensions.DependencyInjection;

namespace Kawe.Abp.Leaning.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var application = AbpApplicationFactory.Create<ConsoleClientModule>())
            {
                application.Initialize();

                var myService = application.ServiceProvider.GetService<IMyService>();
                myService.Run();

                Console.WriteLine("Press ENTER to stop application...");
                Console.ReadLine();

                application.Shutdown();
            }
        }
    }
}
