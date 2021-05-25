using DataIngestion.Src.Services;
using Driver.Src.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Driver
{
    class Program
    {

        static void Main(string[] args)
         {
             Console.WriteLine("Hello World!");
            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            // calls the Run method in App, which is replacing Main
            serviceProvider.GetService<DataIngestionDriver>().Run();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            // var config = LoadConfiguration();
            services.AddSingleton<IMusicService, MediaService>();
            services.AddSingleton<IDataIngestionWebClient, DataIngestionWebClient>();


            // required to run the application
            services.AddTransient<DataIngestionDriver>();

            var myEnv = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.{myEnv}.json", true, true).Build();

            services.AddSingleton(config);
            

            return services;
        }



    }
}
