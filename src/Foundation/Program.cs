﻿using EPiServer.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace Foundation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var isDevelopment = environment == Environments.Development;

            if (isDevelopment)
            {
                Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Warning()
                .WriteTo.File("app_data/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            }


            CreateHostBuilder(args, isDevelopment).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args, bool isDevelopment)
        {
            if (isDevelopment)
            {
                return Host.CreateDefaultBuilder(args)
                    .ConfigureCmsDefaults()
                    .UseSerilog()
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });
            }
            else
            {
                return Host.CreateDefaultBuilder(args)
                    .ConfigureCmsDefaults()
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });
            }
        }
    }
}
