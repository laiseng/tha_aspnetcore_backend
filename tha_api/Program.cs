using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using THA.DBInit;

namespace THA_Api
{
   public class Program
   {
      public static void Main(string[] args)
      {
         Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "THA", "THA.log.txt"),
                 rollingInterval: RollingInterval.Day,
                 rollOnFileSizeLimit: true)
            .CreateLogger();

         try
         {
            Log.Information("Starting web host");

            var host = CreateHostBuilder(args).Build();

            //Populate dummy data into in memory db
            PopulateUsers.PopulateUserIfNotExist(host.Services);
            PopulateProducts.PopulateProductsIfNotExist(host.Services);
            PopulateEmployees.PopulateEmployeesIfNotExist(host.Services);

            host.Run();
         }
         catch (Exception ex)
         {
            Log.Fatal(ex, "Host terminated unexpectedly");
         }
         finally
         {
            Log.CloseAndFlush();
         }
      }



      public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
         .UseSerilog()
              //.ConfigureLogging(logging =>
              // {
              //    logging.ClearProviders();
              //    logging.AddConsole();
              // })
              .ConfigureWebHostDefaults(webBuilder =>
              {
                 webBuilder.UseStartup<Startup>();
              });
   }
}
