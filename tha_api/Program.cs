using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using THA.DBInit;

namespace THA_Api
{
   public class Program
   {
      public static void Main(string[] args)
      {

         var host = CreateHostBuilder(args).Build();

         //Populate dummy data into in memory db
         PopulateUsers.PopulateUserIfNotExist(host);
         PopulateProducts.PopulateProductsIfNotExist(host);

         host.Run();
      }

      public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
         .ConfigureLogging(logging =>
          {
             logging.ClearProviders();
             logging.AddConsole();
          })
              .ConfigureWebHostDefaults(webBuilder =>
              {
                 webBuilder.UseStartup<Startup>();
              });
   }
}
