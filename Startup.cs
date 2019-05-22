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
using System.Text.RegularExpressions;

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
                    options.UseNpgsql(Configuration.GetConnectionString("PASSDB")));

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UsePathBase("/pass");

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
                        ("pass", "pass"),
                        ("localhost:5000", "localhost:5000"),
                        ("data-tool", "data-tool"),
                        ("index", "index"),
                        ("indicator-details", "indicator-details")
                    })
                },
                {
                    "fr-ca",
                    new Translations(new (string, string)[]
                    {
                        ("pass", "apcss"),
                        ("localhost:5000", "127.0.0.1:5000"),
                        ("data-tool", "outil-de-donnees"),
                        ("index", "index"),
                        ("indicator-details", "description-de-mesure")
                    })
                },
            };

            app.Use(async (context, next) =>
                    {
                        var newContent = string.Empty;
                        var existingBody = context.Response.Body;
                        using (var newBody = new MemoryStream())
                        {
                                    // We set the response body to our stream so we can read after the chain of middlewares have been called.
                                    context.Response.Body = newBody;

                            await next();

                                    // Reset the body so nothing from the latter middlewares goes to the output.
                                                    context.Response.Body = existingBody;

                            newBody.Seek(0, SeekOrigin.Begin);

                                    // newContent will be `Hello`.
                                    newContent = new StreamReader(newBody).ReadToEnd();

                                    newContent = Regex.Replace(newContent, @"/(.*)/en-ca/(.*)", "https://health-infobase.canada.ca/" + @"$1" + "/" + @"$2", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                                    newContent = Regex.Replace(newContent, @"/(.*)/fr-ca/(.*)", "https://sante-infobase.canada.ca/" + @"$1" + "/" + @"$2", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                                    
                                    // Send our modified content to the response body.
                                    await context.Response.WriteAsync(newContent);
                        }
                    });

            app.UseMvc(routes =>
             {
                 routes.Routes.Add(new TranslationRoute(
                    translations,
                    routes.DefaultHandler,
                    routeName: null,
                    routeTemplate: "{language=en-ca}/{controller=pass}/{action=Index}/{id?}",
                    defaults: new RouteValueDictionary(new { }),
                    constraints: null,
                    dataTokens: null,
                    inlineConstraintResolver: routes.ServiceProvider.GetRequiredService<IInlineConstraintResolver>()));

             });
        }
    }
}
