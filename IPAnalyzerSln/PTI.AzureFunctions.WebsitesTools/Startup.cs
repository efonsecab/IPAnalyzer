using IPAnalyzer.Services;
using IPAnalyzer.Services.Configuration;
using IPAnalyzer.Services.Interfaces;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

[assembly: FunctionsStartup(typeof(PTI.AzureFunctions.WebsitesTools.Startup))]
namespace PTI.AzureFunctions.WebsitesTools
{
    public class Startup: FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Environment.CurrentDirectory)
               .AddJsonFile("appsettings.json", false)
               .AddUserSecrets(Assembly.GetExecutingAssembly(), false)
               .AddEnvironmentVariables()
               .Build();
            var azureMapsConfiguration = 
                config.GetSection("AzureConfiguration:AzureMapsConfiguration").Get<AzureMapsConfiguration>();
            var ipStackConfiguration = config.GetSection("IpStackConfiguration").Get<IpStackConfiguration>();
            builder.Services.AddSingleton<AzureMapsConfiguration>(azureMapsConfiguration);
            builder.Services.AddSingleton<IpStackConfiguration>(ipStackConfiguration);
            builder.Services.AddTransient<IAzureMapsService, AzureMapsService>();
            builder.Services.AddTransient<IWeatherService, WeatherService>();
            builder.Services.AddTransient<IRobotsService, RobotsService>();
            builder.Services.AddTransient<IGeoLocationService, GeoLocationService>();
        }
    }
}
