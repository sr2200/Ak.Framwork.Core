using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Akf.Core.Web.RestServer
{
    public class AkStartup
    {
        public AkStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. 
        /// Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(optional =>
            {
                optional.Filters.Add(new AkActionFilterAttribute());
                optional.Filters.Add(new AkResultFilterAttribute());
                optional.Filters.Add(new AkExceptionFilterAttribute());
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        /// <summary>
        /// This method gets called by the runtime. 
        /// Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. 
                //You may want to change this for production scenarios, 
                //see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }



            app.UseHttpsRedirection();
            app.UseMvc(routes =>
                {
                    routes.MapRoute(
                            name: "default",
                            template: "api/{controller}"
                        );
                });

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute("default",
            //                    "api/{controller=Values}");
            //    routes.MapRoute("default2",
            //                    "{controller=Values}");
            //});
        }
    }
}
