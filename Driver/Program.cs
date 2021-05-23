using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Driver.Src.DataExtract;

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
            services.AddSingleton<IMusicService, MusicService>();

            // required to run the application
            services.AddTransient<DataIngestionDriver>();

            return services;
        }



    }
}
