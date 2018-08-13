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
                    routes.MapRoute("default", "{culture=EN}/{controller=Strata}/{action=Index}/{id?}");
                    routes.Routes.Add(new CustomRouter(routes.DefaultHandler));
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

        var path = context.HttpContext.Request.Path.Value.Trim('/').Split('/');
        string culture = "EN";
        string action = "Index";
        string controller = "Strata";
        string id = null;
        
        // Culture code has to be decided early
        if (path.Length > 0)
            culture = path[0].ToUpper();

        switch (path.Length) {
            case 2:
                controller = RouteLocalizer.LocalizeRouteElement(culture, path[1].ToLower());
                break;
            case 3:
                action = RouteLocalizer.LocalizeRouteElement(culture, path[2].ToLower());
                goto case 2;
            case 4:
                id = RouteLocalizer.LocalizeRouteElement(culture, path[3].ToLower());
                goto case 3;
            default:
                break;
        }

        context.RouteData.Values["controller"] = controller;
        context.RouteData.Values["action"] = action;
        try
        {
            context.RouteData.Values["id"] = int.Parse(id);
        }
        catch (System.Exception)
        {
            
        }
        
 
        await _defaultRouter.RouteAsync(context);
    }
}
}
