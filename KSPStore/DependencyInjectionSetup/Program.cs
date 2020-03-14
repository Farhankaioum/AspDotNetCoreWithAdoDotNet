using Dna;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DependencyInjectionSetup
{
    public class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // Create a list of dependencies
            var services = new ServiceCollection();


            // Configurations are heavily used in the .Net core DI for
            // configuring services, so we can make use of that
            var configurationBuilder = new ConfigurationBuilder();

            // Add default configuration file
            configurationBuilder.AddJsonFile("appsettings.json", optional:true);

            // Build the configuraton
            var configuration = configurationBuilder.Build();

            // Inject the configuration into the DI system
            services.AddSingleton<IConfiguration>(configuration);

            // At this point, all dependencies can be added to the
            // DI system via the service collection

            // e.g. services.AddScoped()

            // Builder service provider
            var provider = services.BuildServiceProvider();

            // At this point, your DI system is ready to go
            // and you can get any services via the provider
            var myConfiguration = provider.GetService<IConfiguration>();



            // The way to do this in Dna.Framework.....

            // Use framework.Construct<FrameworkConstruction> for totally
            // blank service provider containing just the FrameworkEnvironment
            // but no configuration or other services
            // For this purpose using a class with blank constructor and inherit FramworkConstruction class
            // then pass this class name .Construct<className>()

            // Or you can use DefaultframeworkConstruction for one that
            // includes basic services such as logging and inclue a 
            // configuration
            Framework.Construct<DefaultFrameworkConstruction>()
                // Add further services
                .AddFileLogger()
                // And once done, build
                .Build();
        }
    }

    // If want use FrameworkConstruction, then need a class with blank constructor and inherit FrameworkConstruction class
    public class BlankFrameworkConstruction : FrameworkConstruction
    {
        public BlankFrameworkConstruction()
        {

        }
    }
}
