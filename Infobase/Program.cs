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


            // using (var webhost = CreateTestWebHostBuilder(args).Build())
            // {
            //     webhost.Start();

            //     var server = webhost.GetTestServer();
            //     var client = server.CreateClient();
            //     client.DefaultRequestHeaders.Add("host", "french.localhost:5000");

            //     var path = "/pass/sitemap";
            //     var response = await client.GetAsync(path);
            //     var body = await response.Content.ReadAsStringAsync();

            //     File.WriteAllText("../Output/a", body);
            // }

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //.UseUrls("http://0.0.0.0:" +  Program.Port.ToString())
                .UseStartup<Startup>();

        public static IWebHostBuilder CreateTestWebHostBuilder(string[] args) => CreateWebHostBuilder(args).UseTestServer();
    }
}
