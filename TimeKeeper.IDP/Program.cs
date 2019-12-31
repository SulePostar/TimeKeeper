﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TimeKeeper.IDP
{
    public class Program
    {
        public static void Main()
        {
            CreateWebHostBuilder().Build().Run();
            //var host = new WebHostBuilder()
            //         .UseKestrel()
            //         .UseContentRoot(Directory.GetCurrentDirectory())
            //         .UseIISIntegration()
            //         .UseStartup<Startup>()
            //         .Build();
            //host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder() =>
            WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>();
    }
}
