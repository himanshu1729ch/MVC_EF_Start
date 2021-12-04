using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MVC_EF_Start
{
  public class Program
  {
    public static void Main(string[] args)
    {
            //BuildWebHost(args).Run();
            CreateWebHostBuilder(args).Build().Run();
    }

       /* private static object CreateWebHostBuilder(string[] args)
        {
            throw new NotImplementedException();
        }*/
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();

        /*public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .Build();*/
    }
}
