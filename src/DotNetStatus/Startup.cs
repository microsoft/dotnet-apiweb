// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Fx.Portability;
using System;
using System.Net.Http;

namespace DotNetStatus.vNext
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IHostingEnvironment env)
        {
            // Setup configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            // Add the following to the request pipeline only in development environment.
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Add Error handling middleware which catches all application specific errors and
                // send the request to the following path or controller action.
                app.UseExceptionHandler("/Home/Error");
            }

            // Add static files to the request pipeline
            app.UseStaticFiles();

            // Add MVC to the request pipeline (we use attribute routing)
            app.UseMvc();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInstance(new ProductInformation("DotNetStatus"));
            services.AddSingleton<IApiPortService>(CreateService);
            services.AddSingleton<PortabilityToolsStatusService>(CreateJsonDataService);

            // Add MVC services to container
            services.AddMvc();
            services.AddLogging();
        }

        private IApiPortService CreateService(IServiceProvider arg)
        {
            return new ApiPortService(GetApiPortUrl(), arg.GetRequiredService<ProductInformation>());
        }

        private PortabilityToolsStatusService CreateJsonDataService(IServiceProvider arg)
        {
            return new PortabilityToolsStatusService(CreateHttpClient(), arg.GetRequiredService<ProductInformation>());
        }

        /// <summary>
        /// Get's the URL from ApiPort setting in appsettings.json
        /// </summary>
        private string GetApiPortUrl()
        {
            string endpoint = _configuration["ApiPortService:Url"];
            if (string.IsNullOrEmpty(endpoint))
            {
                throw new ArgumentNullException("ApiPortService", "Need to specify ApiPortService in appsettings.json");
            }

            return endpoint;
        }

        /// <summary>
        /// Creates an HTTPClient with the appsettings.json Url
        /// </summary>
        private HttpClient CreateHttpClient()
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(GetApiPortUrl());
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "AnalyzerStatusCheck");

            return client;
        }

    }
}
