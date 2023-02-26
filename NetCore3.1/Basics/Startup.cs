using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basics
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; } // Read variables in appsettings.json, lauchSettings.json, 

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Use ILogger as parameter to apply logging
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            /*
            // Middleware App.Run(): Get a static response of HTTP Context
            // App.Run() is a terminal middleware por esa razon despues de este no pasa a otro middleware.
            app.Run(async (context) =>
            {
                //See process name
                await context.Response.WriteAsync(System.Diagnostics.Process.GetCurrentProcess().ProcessName);
                
                //Read value from IConfiguration
                await context.Response.WriteAsync(Configuration["MyKey"]);
            });

            //Use to request the next middleware
            app.Use(async (context, next) =>
            { 
                await context.Response.WriteAsync("1st Middleware");
                // Next is a delagate that helps to call the next middleware
                await next();
            });
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("2nd Middleware");
            });

                        //Use to request the next middleware using Logger (See Output Window - Show output from: Debug)
            app.Use(async (context, next) =>
            {
                logger.LogInformation("MW1: Incoming Request");
                await next();
                logger.LogInformation("MW1: Outgoing Response");
            });
            app.Use(async (context, next) =>
            {
                logger.LogInformation("MW2: Incoming Request");
                await next();
                logger.LogInformation("MW2: Outgoing Response");
            });
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("MW3: Request handled and response produced");
                logger.LogInformation("MW3: Request handled and response produced");
            });

            // Output Result: When a middleware handles the request and produces response, the request processing pipeline starst to reverse.
            //    Basics.Startup: Information: MW1: Incoming Request
            //    Basics.Startup: Information: MW2: Incoming Request
            //    Basics.Startup: Information: MW3: Request handled and response produced
            //    Basics.Startup: Information: MW2: Outgoing Response
            //    Basics.Startup: Information: MW1: Outgoing Response
           
            */



            app.UseHttpsRedirection();
            app.UseStaticFiles(); // Middleware that let access (Eg. By code by relative path or URL in the browser) to static files in wwwroot folder

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
