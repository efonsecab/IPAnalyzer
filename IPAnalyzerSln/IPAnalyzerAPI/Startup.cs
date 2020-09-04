using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using IPAnalyzerAPI.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace IPAnalyzerAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddTransient<HttpClient>();
            services.AddControllers();
            services.AddCors((options) =>
            {
                options.AddPolicy("trustedDomains",
                    policy =>
                    {
                        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
            });
            BindConfiguration(services);
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(majorVersion: 1, minorVersion: 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
            });
            services.AddSwaggerGen(c =>
            {
                c.UseInlineDefinitionsForEnums();
                //check https://github.com/domaindrivendev/Swashbuckle.AspNetCore#generate-multiple-swagger-documents
                c.SwaggerDoc("WebsitesTools", new OpenApiInfo { Title = "Websites Tools APIs", Version = "v1" });
                var filePath = Path.Combine(System.AppContext.BaseDirectory, 
                    $"{Assembly.GetExecutingAssembly().GetName()}.xml");
                if (System.IO.File.Exists(filePath))
                    c.IncludeXmlComments(filePath);
            });
        }


        private void BindConfiguration(IServiceCollection services)
        {
            ServicesConfiguration servicesConfiguration = Configuration.GetSection("ServicesConfiguration").Get<ServicesConfiguration>();
            services.AddSingleton(servicesConfiguration);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(options =>
            {
                options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/WebsitesTools/swagger.json", "Websites Tools V1");
                c.RoutePrefix = "";
            });
        }
    }
}
