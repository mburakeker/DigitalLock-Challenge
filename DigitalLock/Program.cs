using DigitalLock.Interfaces;
using DigitalLock.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace DigitalLock
{
    static class Program
    {
        public static IConfigurationRoot configuration;
        static void Main(string[] args)
        {
            //setup our DI
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureServices();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var lockPick = serviceProvider.GetService<IPickLock>();
            var digitalLock = serviceProvider.GetService<IDigitalLock>();

            lockPick.Unlock(digitalLock);
        }
        public static void AddDigitalLockModel(this IServiceCollection serviceCollection)
        {
            serviceCollection.Configure<DigitalLockModel>(options =>  configuration.GetSection("DigitalLock").Bind(options));
        }
        private static void ConfigureServices(this IServiceCollection serviceCollection)
        {
            // Build configuration
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            // Add access to generic IConfigurationRoot
            serviceCollection.AddSingleton<IConfigurationRoot>(configuration);
            serviceCollection.AddLogging();
            serviceCollection.AddSingleton<IDigitalLock, DigitalLock>();
            serviceCollection.AddDigitalLockModel();
            serviceCollection.AddSingleton<IPickLock, PickLock>();

        }
    }
}
