using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using THA.Entity.Main;
using THA.Model.Core;
using THA.Model.Product;

namespace THA.DBInit
{
   public class PopulateProducts
   {
      public static void PopulateProductsIfNotExist(IHost host)
      {
         using (var scope = host.Services.CreateScope())
         {
            var services = scope.ServiceProvider;
            try
            {
               var context = services.GetRequiredService<MainContext>();
               Populate(context);
            }
            catch (Exception ex)
            {
               var logger = services.GetRequiredService<ILogger<MainContext>>();
               logger.LogError(ex, "error when populating Products ");
            }
         }

      }

      public static void Populate(MainContext context)
      {
         context.Database.EnsureCreated();

         if (context.Products.Any())
         {
            // DB has been populated nothing to do here
            return;
         }

         var products = new ProductModel[]
         {
            new ProductModel{
               ID = 1,
               CREATE_BY = 0,
               EDIT_BY = 0,
               CREATE_DATE = DateTime.UtcNow,
               EDIT_DATE = DateTime.UtcNow,
               STATUS = Statuses.NEW,
               DESCRIPTION="Red Ball",
               NAME="Red Ball",
               PRICE="10"
            },
            new ProductModel{
               ID = 2,
               CREATE_BY = 0,
               EDIT_BY = 0,
               CREATE_DATE = DateTime.UtcNow,
               EDIT_DATE = DateTime.UtcNow,
               STATUS = Statuses.NEW,
               DESCRIPTION="Green Ball",
               NAME="Green Ball",
               PRICE="20"
            },
            new ProductModel{
               ID = 3,
               CREATE_BY = 0,
               EDIT_BY = 0,
               CREATE_DATE = DateTime.UtcNow,
               EDIT_DATE = DateTime.UtcNow,
               STATUS = Statuses.NEW,
               DESCRIPTION="Blue Ball",
               NAME="Blue Ball",
               PRICE="30"
            },
         };

         foreach (var p in products)
         {
            context.Products.Add(p);
         }

         var sresult = context.SaveChanges();
      }
   }
}
