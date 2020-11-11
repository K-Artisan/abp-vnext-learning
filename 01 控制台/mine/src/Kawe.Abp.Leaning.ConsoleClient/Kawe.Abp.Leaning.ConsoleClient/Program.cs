using System;
using Volo.Abp;

namespace Kawe.Abp.Leaning.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var application = AbpApplicationFactory.Create<ConsoleClientModule>())
            {
                application.Initialize();

                var myService = (IMyService)application.ServiceProvider.GetService(typeof(IMyService));
                myService.Run();

                Console.WriteLine("Press ENTER to stop application...");
                Console.ReadLine();

                application.Shutdown();
            }
        }
    }
}
