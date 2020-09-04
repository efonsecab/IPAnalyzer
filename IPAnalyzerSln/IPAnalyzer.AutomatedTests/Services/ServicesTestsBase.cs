using IPAnalyzer.Services;
using IPAnalyzer.Services.Configuration;
using IPAnalyzer.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace IPAnalyzer.AutomatedTests.Services
{
    public abstract class ServicesTestsBase
    {
        public ServicesTestsBase()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.Development.json")
                .AddUserSecrets("8a992b05-3615-4e14-9cd3-ad0af607ca3f");
            IConfiguration configuration = configurationBuilder.Build();
            this.Configuration = configuration;
            var azureMapsConfiguration = configuration.GetSection("AzureConfiguration:AzureMapsConfiguration").Get<AzureMapsConfiguration>();
            this.AzureMapsConfiguration = azureMapsConfiguration;
            //this.Server = new TestServer(new WebHostBuilder()
            //    .UseConfiguration(configuration)
            //    .UseStartup<Startup>())
            //    );
            this.Services = new ServiceCollection();
            this.Services.AddHttpClient();
            this.Services.AddSingleton(this.AzureMapsConfiguration);
            this.Services.AddTransient<HttpClient>();
            //this.Services.AddTransient<ILogger<AzureMapsService>, Logger<AzureMapsService>>();
            //this.Services.AddTransient<ILogger<WeatherService>, Logger<WeatherService>>();
            this.Services.AddTransient<IAzureMapsService, AzureMapsService>();
            this.Services.AddTransient<IWeatherService, WeatherService>();
            this.ServiceProvider = this.Services.BuildServiceProvider();
            //this.HttpClientFactory = this.Services.BuildServiceProvider().GetRequiredService<IHttpClientFactory>();
            this.AzureMapsService = this.ServiceProvider.GetRequiredService<IAzureMapsService>();
            this.WeatherService = this.ServiceProvider.GetRequiredService<IWeatherService>();
            //this.ServerClient = this.Server.CreateClient();
        }

        private IConfiguration Configuration { get; }
        internal protected AzureMapsConfiguration AzureMapsConfiguration { get; }
        internal protected TestServer Server { get; }
        internal protected ServiceCollection Services { get; }
        public ServiceProvider ServiceProvider { get; }
        public IAzureMapsService AzureMapsService { get; }
        public IWeatherService WeatherService { get; }
        internal protected HttpClient ServerClient { get; }
    }
}
