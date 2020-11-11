using System;
using Volo.Abp;
using Microsoft.Extensions.DependencyInjection;
using Kawe.Abp.Learning.AccountAbpModule;
using Volo.Abp.Modularity.PlugIns;
using System.Net.Mail;

namespace Kawe.Abp.Leaning.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var application = AbpApplicationFactory.Create<ConsoleClientModule>(options =>
            {
                options.PlugInSources.AddFolder(@$"{AppDomain.CurrentDomain.BaseDirectory}/MyPlugIns");
            }))
            {
                application.Initialize();

                var myService = application.ServiceProvider.GetService<IMyService>();
                myService.Run();

                var accountService = application.ServiceProvider.GetService<AccountService>();
                accountService.Run();

                Console.WriteLine("Press ENTER to stop application...");
                Console.ReadLine();

                application.Shutdown();
            }
        }
    }
}
