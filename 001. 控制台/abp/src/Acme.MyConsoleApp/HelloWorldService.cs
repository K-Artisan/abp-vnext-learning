using System;
using Volo.Abp.DependencyInjection;

namespace Acme.MyConsoleApp
{
    public class HelloWorldService : ITransientDependency
    {
        public void SayHello()
        {
            Console.WriteLine("Hello World!");
        }
    }
}
