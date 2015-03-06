// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;
using Microsoft.Fx.Portability;
using System;

namespace DotNetStatus.vNext
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Configuration = new Configuration()
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
        }

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            // Add the following to the request pipeline only in development environment.
            if (string.Equals(env.EnvironmentName, "Development", StringComparison.OrdinalIgnoreCase))
            {
                app.UseBrowserLink();
                app.UseErrorPage(ErrorPageOptions.ShowAll);
            }
            else
            {
                // Add Error handling middleware which catches all application specific errors and
                // send the request to the following path or controller action.
                app.UseErrorHandler("/Home/Error");
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

            // Add MVC services to container
            services.AddMvc();
        }

        private IApiPortService CreateService(IServiceProvider arg)
        {
            string endpoint = null;
            if (!Configuration.TryGet("ApiPortService", out endpoint))
            {
                throw new ArgumentNullException("ApiPortService", "Need to specify ApiPortService in config.json");
            }

            return new ApiPortService(endpoint, arg.GetRequiredService<ProductInformation>());
        }
    }
}
