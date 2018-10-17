using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JavaScriptEngineSwitcher.ChakraCore;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using React.AspNet;
using Infobase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;
using Infobase.Common;
using System.IO;

namespace Infobase
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddJsEngineSwitcher(options => options.DefaultEngineName = ChakraCoreJsEngine.EngineName)
                .AddChakraCore();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddReact();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<PASSContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("PASSDB")));

            string createText = "Hello and Welcome " + Configuration.GetConnectionString("PASSDB");
            File.WriteAllText("./test.txt", createText);
            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UsePathBase("/pass");
            // Remove me
            app.UseDeveloperExceptionPage();

            // Uncomment
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            //
            app.UseReact(config =>
            {
                config
                    .SetLoadReact(true)
                    .SetLoadBabel(false)
                    .SetReuseJavaScriptEngines(false)
                    .AddScriptWithoutTransform("~/js/app.js");
            });

            app.UseStaticFiles();

            var translations = new Dictionary<string, Translations>(StringComparer.OrdinalIgnoreCase)
            {
                {
                    "en-ca",
                    new Translations(new (string, string)[]
                    {
                        ("Strata", "Strata"),
                        ("Datatool", "Datatool"),
                        ("Index", "Index"),
                        ("Details", "Details")
                    })
                },
                {
                    "fr",
                    new Translations(new (string, string)[]
                    {
                        ("Strata", "StrataFr"),
                        ("Datatool", "DatatoolFr"),
                        ("Index", "IndexFr"),
                        ("Details", "DetailsFr")
                    })
                },
            };

            app.UseMvc(routes =>
             {
                 routes.Routes.Add(new TranslationRoute(
                    translations,
                    routes.DefaultHandler,
                    routeName: null,
                    routeTemplate: "{language=en-ca}/{controller=Strata}/{action=Index}/{id?}",
                    defaults: new RouteValueDictionary(new {  }),
                    constraints: null,
                    dataTokens: null,
                    inlineConstraintResolver: routes.ServiceProvider.GetRequiredService<IInlineConstraintResolver>()));
                 
             });
        }
    }
}
