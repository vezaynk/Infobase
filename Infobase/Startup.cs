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
using Microsoft.Extensions.Hosting;
using React.AspNet;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Models.Metadata;
using System.Text.Json;

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

            services.AddReact();
            services.AddHttpContextAccessor();
            services.AddSingleton<I18nTransformer>();

            services.AddControllersWithViews()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DictionaryKeyPolicy = PreserveCasePolicy.Policy;
            });

            services.AddRouting();

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
                if (connectionString == null)
                {
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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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



            // app.UseMiniProfiler();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                var i18nTransformer = endpoints.ServiceProvider.GetRequiredService<I18nTransformer>();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{datatool}/{page}/{id?}",
                    defaults: new { controller = "Open", action = "Index", page = "Index" },
                    constraints: new { datatool = i18nTransformer, page = i18nTransformer }
                );

                endpoints.MapControllerRoute("Index listing", "/", new { controller = "Open", action = "List" });
            });
        }
    }

    public class PreserveCasePolicy : JsonNamingPolicy
    {
        public static PreserveCasePolicy Policy => new PreserveCasePolicy();

        public override string ConvertName(string name) => name;
    }

}