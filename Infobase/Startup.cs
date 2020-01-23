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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using React.AspNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;
using System.Reflection;
using Models.Metadata;
using Microsoft.AspNetCore.Mvc.Razor;
using CommandLine;

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
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services
                .AddJsEngineSwitcher(options => options.DefaultEngineName = ChakraCoreJsEngine.EngineName)
                .AddChakraCore();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddReact();
            services.AddMvc();

            var dbContextTypes = Assembly
                                    .GetExecutingAssembly()
                                    .GetReferencedAssemblies()
                                    .Select(x => Assembly.Load(x))
                                    .SelectMany(a => a.GetTypes())
                                    .Where(t => t.BaseType == typeof(DbContext) && t.GetCustomAttribute<DatabaseAttribute>() != null);

            var dbSetLookup = new Dictionary<string, SortedDictionary<Type, ICollection<dynamic>>>();
            
            Console.WriteLine($"Found {dbContextTypes.Count()} contexts");
            foreach (var dbContextType in dbContextTypes)
            {
                var name = dbContextType.GetCustomAttribute<DatabaseAttribute>().DatabaseName;
                Console.WriteLine($"Loading {name}");
                var databaseName = dbContextType.GetCustomAttribute<DatabaseAttribute>().DatabaseName;
                var connectionString = Configuration.GetConnectionString(databaseName);
                if (connectionString == null) {   
                    Console.WriteLine($"Missing connection string for {name}", Console.Error);
                    continue;
                }
                // Generic OptionBuilder type to work with the loaded DbContext 
                Type optionBuilderType = typeof(DbContextOptionsBuilder<>).MakeGenericType(new Type[] { dbContextType });
                // Instance of optionBuilder
                DbContextOptionsBuilder optionsBuilder = (DbContextOptionsBuilder)Activator.CreateInstance(optionBuilderType);

                optionsBuilder.UseNpgsql(connectionString);

                // Create dbContext using configured optionBuilder
                var dbContext = (DbContext)Activator.CreateInstance(dbContextType, new object[] {
                    optionsBuilder.Options
                });


                IEnumerable<dynamic> GetDbSet(Type setType)
                {
                    var genericDbSetMethod = dbContextType.GetMethod("Set").MakeGenericMethod(new[] { setType });
                    return Enumerable.Cast<dynamic>((IEnumerable)genericDbSetMethod.Invoke(dbContext, new object[] { }));
                }

                var dbSets = dbContextType.Assembly.GetTypes()
                // Load all models, excluding the non-filter ones (Only the Master is excluded).
                .Where(t => t.Namespace == dbContextType.Namespace && t.GetCustomAttribute<FilterAttribute>() != null)
                .OrderByDescending(t => t.GetCustomAttribute<FilterAttribute>().Level);

                var dataCache = new SortedDictionary<Type, ICollection<dynamic>>(Comparer<Type>.Create((a, b) => a.GetCustomAttribute<FilterAttribute>().Level - b.GetCustomAttribute<FilterAttribute>().Level));

                foreach (Type dsType in dbSets)
                {
                    var list = GetDbSet(dsType).OrderBy(row => row.Index).ToList();
                    list.Sort((a, b) => a.Index - b.Index);
                    foreach (var property in dsType.GetProperties().Where(p => p.GetCustomAttribute<ChildrenAttribute>() != null))
                    {
                        foreach (var item in list)
                        {
                            var children = property.GetValue(item);
                            var childType = property.PropertyType.GetGenericArguments()[0];

                            if (children != null)
                            {
                                var sortedChildren = Enumerable.Cast<dynamic>((IEnumerable)children).OrderBy(row => row.Index).ToList();

                                var retypedChildren = (typeof(Enumerable).GetMethod("Cast").MakeGenericMethod(new[] { childType }).Invoke(typeof(Enumerable), new[] { sortedChildren }));
                                property.SetValue(item, typeof(Enumerable).GetMethod("ToList").MakeGenericMethod(new[] { childType }).Invoke(retypedChildren, new object[] { retypedChildren }));
                            }
                        }
                    }
                    dataCache.Add(dsType, list);
                }
                dbSetLookup.Add(databaseName, dataCache);
            }
            Console.WriteLine("Done");

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
                    .SetReuseJavaScriptEngines(true)
                    .SetStartEngines(10)
                    .SetMaxUsagesPerEngine(0)
                    .SetMaxEngines(25);
            });

            app.UseStaticFiles();

            var translations = new Dictionary<string, Translations>(StringComparer.OrdinalIgnoreCase)
            {
                {
                    "en-ca",
                    new Translations(new (string, string)[]
                    {
                        ("pass", "pass"),
                        ("pass2", "pass2"),
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
                        ("pass2", "apcss2"),
                        ("data-tool", "outil-de-donnees"),
                        ("index", "index"),
                        ("indicator-details", "description-de-mesure")
                    })
                },
            };
            
            // app.UseMiniProfiler();

            app.UseMvc(routes =>
             {
                 routes.Routes.Add(new TranslationRoute(
                    translations,
                    new Dictionary<string, string> {
                        { "english.localhost:5000", "en-ca"},
                        { "french.localhost:5000", "fr-ca"}
                    },
                    "en-ca",
                    routes.DefaultHandler,
                    routeName: null,
                    routeTemplate: "/{datatool}/{action}/{id?}",
                    defaults: new RouteValueDictionary(new { controller = "open" }),
                    constraints: null,
                    dataTokens: null,
                    inlineConstraintResolver: routes.ServiceProvider.GetRequiredService<IInlineConstraintResolver>()));

                 routes.Routes.Add(new TranslationRoute(
                    translations,
                    new Dictionary<string, string> {
                        { "english.localhost:5000", "en-ca"},
                        { "french.localhost:5000", "fr-ca"}
                    },
                    "en-ca",
                    routes.DefaultHandler,
                    routeName: null,
                    routeTemplate: "/",
                    defaults: new RouteValueDictionary(new { controller = "open", action = "list", language = "en-ca"}),
                    constraints: null,
                    dataTokens: null,
                    inlineConstraintResolver: routes.ServiceProvider.GetRequiredService<IInlineConstraintResolver>()));
             });
        }
    }

}