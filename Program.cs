using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Infobase.Automation;
namespace Infobase
{
    public class Program
    {
        private static int Port {get;set;} = 8080;
        public static void Main(string[] args)
        {
            while (true) {
                ModelParser.GetModelsByDataset("PASS");
                Console.ReadLine();
            }
            string portEnv = System.Environment.GetEnvironmentVariable("PORT");
            int n = 8080;
            bool isNumeric = int.TryParse(portEnv, out n);
            if (isNumeric)
                Program.Port = n;
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //.UseUrls("http://0.0.0.0:" +  Program.Port.ToString())
                .UseStartup<Startup>();
    }
}
