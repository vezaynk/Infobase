using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using CommandLine;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Models.Metadata;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Infobase
{
    public class Program
    {
        private static int Port { get; set; } = 8080;
        public static async Task Main(string[] args)
        {
            string portEnv = System.Environment.GetEnvironmentVariable("PORT");
            int n = 8080;
            bool isNumeric = int.TryParse(portEnv, out n);
            if (isNumeric)
                Program.Port = n;


            using (var webhost = CreateTestWebHostBuilder(args).Build())
            {
                webhost.Start();

                var server = webhost.GetTestServer();
                var client = server.CreateClient();

                async Task<string> GetUrl(string path)
                {
                    var response = await client.GetAsync(path);
                    var body = await response.Content.ReadAsStringAsync();
                    return body;
                }

                var path = "http://localhost:5000/cmsif/sitemap";


                var sitemapbody = await GetUrl(path);
                var pages = JsonSerializer.Deserialize<IList<string>>(sitemapbody);

                foreach (var pageUrl in pages) {
                    var savePath = "../Output/" + pageUrl.Replace("?", "@");
                    Directory.CreateDirectory(savePath);

                    var pageBody = await GetUrl(pageUrl);
                    
                    File.WriteAllText(savePath + "/index.html", pageBody);
                }
                
            }

            //CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //.UseUrls("http://0.0.0.0:" +  Program.Port.ToString())
                .UseStartup<Startup>()
                .ConfigureServices(services =>
                {
                    //services.AddSingleton()
                });

        public static IWebHostBuilder CreateTestWebHostBuilder(string[] args) => CreateWebHostBuilder(args).UseTestServer();
    }
}
