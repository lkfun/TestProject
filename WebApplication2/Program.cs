using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApplication2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) { 
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseWebRoot("/home/lk/文档/PublishOutput7/wwwroot")
                .Build();
        }
    }
}
