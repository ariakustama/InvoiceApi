using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace InvoiceAPI
{
    public class Program
    {
        private static readonly string LOGGER_OUTPUT_TEMPLATE = "[{Timestamp:o}] [{Level:u3}] ({Application}/{MachineName}/{ThreadId}) {Message}{NewLine}{Exception}";
        private static LogEventLevel level = LogEventLevel.Information;
        public static void Main(string[] args)
        {
            string loggerFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Log", $"Invoice_API.log");
            #if DEBUG
                level = LogEventLevel.Debug;
            #endif
            Log.Logger = CreateDefaultLogger(/*config, */loggerFilePath, level);

            try
            {
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                Log.Fatal(ex.Message);
                throw new ApplicationException("Application terminated");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseSerilog();
                });

        private static Logger CreateDefaultLogger(string loggerFilePath, LogEventLevel level) =>
          new LoggerConfiguration()
                #if DEBUG
                    .MinimumLevel.Debug()
                #endif
               .Enrich.WithProperty("Application", "Invoice_API")
              .Enrich.FromLogContext()
              .Enrich.WithMachineName()
              .Enrich.WithThreadId()
              .WriteTo.Console(outputTemplate: LOGGER_OUTPUT_TEMPLATE, restrictedToMinimumLevel: level)
              .WriteTo.File(loggerFilePath,
                           restrictedToMinimumLevel: level,
                           rollingInterval: RollingInterval.Day,
                           outputTemplate: LOGGER_OUTPUT_TEMPLATE,
                           fileSizeLimitBytes: 512000000,
                           rollOnFileSizeLimit: true)
              .CreateLogger();
    }
}
