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
                    options.UseSqlServer(Configuration.GetConnectionString("PASSContext")));
            
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
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseReact(config =>
            {
                config
                    .SetLoadReact(true)
                    .SetLoadBabel(false)
                    .SetReuseJavaScriptEngines(false)
                    .AddScriptWithoutTransform("~/js/app.js");
            });
                    
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            
            app.UseMvc(routes =>
            {
                routes.Routes.Add(new CustomRouter(routes.DefaultHandler));
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    public class CustomRouter : IRouter
{
    private IRouter _defaultRouter;

    public CustomRouter(IRouter defaultRouteHandler)
    {
        _defaultRouter = defaultRouteHandler;
    }

    public VirtualPathData GetVirtualPath(VirtualPathContext context)
    {
        return _defaultRouter.GetVirtualPath(context);
    }

    public async Task RouteAsync(RouteContext context)
    {

        var requestedController = context.RouteData.Values["controller"];
        var requestedAction = context.RouteData.Values["action"];
        var requestedLanguage = context.RouteData.Values["culture"];
        Console.WriteLine(requestedController);
        //context.RouteData.Values["controller"] = "Strata";
          //  context.RouteData.Values["action"] = "Index";

            await _defaultRouter.RouteAsync(context);
        // Look for the User-Agent Header and Check if the Request comes from a Mobile 
        /*if (headers.ContainsKey("User-Agent") &&
            headers["User-Agent"].ToString().Contains("Mobile"))
        {
            var action = "Index";
            var controller = "";
            if (path.Length > 1)
            {
                controller = path[1];
                if (path.Length > 2)
                    action = path[2];
            }

            context.RouteData.Values["controller"] = $"Mobile{controller}";
            context.RouteData.Values["action"] = action;

            await _defaultRouter.RouteAsync(context);
        }*/
    }
}
}
