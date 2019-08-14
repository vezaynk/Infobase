using System;
using System.Collections.Generic;
using System.Collections;
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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
using Models.Metadata;

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

            var dbContextTypes = Assembly
                                    .GetExecutingAssembly()
                                    .GetReferencedAssemblies()
                                    .Select(x => Assembly.Load(x))
                                    .SelectMany(a => a.GetTypes())
                                    .Where(t => t.BaseType == typeof(DbContext) && t.Namespace.StartsWith("Models.Contexts"));

            Console.WriteLine($"{dbContextTypes.Count()} contexts");
            
            var dbSetLookup = new Dictionary<string, SortedDictionary<Type, IEnumerable>>();
            foreach (var dbContextType in dbContextTypes)
            {
                var databaseName = dbContextType.GetCustomAttribute<DatabaseAttribute>().DatabaseName;
                var connectionString = Configuration.GetConnectionString(databaseName);

                // Generic OptionBuilder type to work with the loaded DbContext 
                Type optionBuilderType = typeof(DbContextOptionsBuilder<>).MakeGenericType(new Type[] { dbContextType });
                // Instance of optionBuilder
                DbContextOptionsBuilder optionsBuilder = (DbContextOptionsBuilder)Activator.CreateInstance(optionBuilderType);

                optionsBuilder.UseNpgsql(connectionString);

                // Create dbContext using configured optionBuilder
                var dbContext = (DbContext)Activator.CreateInstance(dbContextType, new object[] {
                    optionsBuilder.Options
                });


                IEnumerable<object> GetDbSet(Type setType)
                {
                    var genericDbSetMethod = dbContextType.GetMethod("Set").MakeGenericMethod(new[] { setType });
                    return Enumerable.Cast<object>((IEnumerable)genericDbSetMethod.Invoke(dbContext, new object[] { }));
                }

                var dbSets = dbContextType.Assembly.GetTypes()
                // Load all models, excluding the non-filter ones (Only the Master is excluded).
                .Where(t => t.Namespace == dbContextType.Namespace && t.GetCustomAttribute<FilterAttribute>() != null)
                .ToList();

                var dataCache = new SortedDictionary<Type, IEnumerable>(Comparer<Type>.Create((a, b) => a.GetCustomAttribute<FilterAttribute>().Level - b.GetCustomAttribute<FilterAttribute>().Level));
                foreach (Type dsType in dbSets) {
                    var indexProperty = dsType.GetProperty("Index");
                    var list = GetDbSet(dsType).OrderBy(row => dsType.GetProperty("Index").GetValue(row)).ToList();
                    list.Sort((a, b) => (int)indexProperty.GetValue(a) - (int)indexProperty.GetValue(b));
                    dataCache.Add(dsType, list);
                }
                dbSetLookup.Add(databaseName, dataCache);
            }
            services.AddSingleton(dbSetLookup);

            //services.AddMiniProfiler();

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // TODO: Use real error pages
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseReact(config =>
            {
                config
                    .SetLoadReact(false)
                    .SetAllowJavaScriptPrecompilation(true)
                    .AddScriptWithoutTransform("~/js/server.js")
                    .SetReuseJavaScriptEngines(false);
            });

            app.UseStaticFiles();

            var translations = new Dictionary<string, Translations>(StringComparer.OrdinalIgnoreCase)
            {
                {
                    "en-ca",
                    new Translations(new (string, string)[]
                    {
                        ("pass", "pass"),
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
                        ("data-tool", "outil-de-donnees"),
                        ("index", "index"),
                        ("indicator-details", "description-de-mesure")
                    })
                },
            };
            // breaks due to context.Response.ContentLength mismatch
            // app.UseMiniProfiler();
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

                            newContent = new StreamReader(newBody).ReadToEnd();

                            newContent = Regex.Replace(newContent, @"/(.*)/en-ca/(.*)", "https://health-infobase.canada.ca/" + @"$2", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                            newContent = Regex.Replace(newContent, @"/(.*)/fr-ca/(.*)", "https://sante-infobase.canada.ca/" + @"$2", RegexOptions.IgnoreCase | RegexOptions.Compiled);

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
                    defaults: new RouteValueDictionary(new { test = 1 }),
                    constraints: null,
                    dataTokens: null,
                    inlineConstraintResolver: routes.ServiceProvider.GetRequiredService<IInlineConstraintResolver>()));

             });
        }
    }

}