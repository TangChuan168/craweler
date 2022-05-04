using System;
using ConsoleSpider.Domain.Contracts;
using ConsoleSpider.Domain.Models;
using ConsoleSpider.Domain.Services;
using Guru99Demo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleSpider
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var dataService = ActivatorUtilities.CreateInstance<CGuru99Demo>(host.Services);
            dataService.startBrowser();
            dataService.test();

        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
                    services.AddScoped<Dbcontext>();
                    services.AddScoped<CGuru99Demo>();
                    services.AddScoped<CateService>();
                });
        }
    }
}
