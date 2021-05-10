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
      public static DateTime MOCK_DB_ROW_DATE = DateTime.Parse("2021-01-10T05:24:34.088Z");
      public static ProductModel[] MOCK_PRODUCTS = new ProductModel[]
        {
            new ProductModel{
               ID = new Guid("bbbbb5aa-b5cc-4ae3-9c96-4ab8b969db90"),
               CREATE_BY =  new Guid("00000000-0000-0000-0000-000000000000"),
               EDIT_BY =  new Guid("00000000-0000-0000-0000-000000000000"),
               CREATE_DATE = MOCK_DB_ROW_DATE,
               EDIT_DATE = MOCK_DB_ROW_DATE,
               STATUS = Statuses.NEW,
               DESCRIPTION="Red Ball",
               NAME="Red Ball",
               PRICE="10"
            },
            new ProductModel{
               ID = new Guid("bbbbb5aa-b5cc-4ae3-9c96-4ab8b969db91"),
               CREATE_BY =  new Guid("00000000-0000-0000-0000-000000000000"),
               EDIT_BY =  new Guid("00000000-0000-0000-0000-000000000000"),
               CREATE_DATE = MOCK_DB_ROW_DATE,
               EDIT_DATE = MOCK_DB_ROW_DATE,
               STATUS = Statuses.NEW,
               DESCRIPTION="Green Ball",
               NAME="Green Ball",
               PRICE="20"
            },
            new ProductModel{
               ID = new Guid("bbbbb5aa-b5cc-4ae3-9c96-4ab8b969db92"),
               CREATE_BY =  new Guid("00000000-0000-0000-0000-000000000000"),
               EDIT_BY =  new Guid("00000000-0000-0000-0000-000000000000"),
               CREATE_DATE = MOCK_DB_ROW_DATE,
               EDIT_DATE = MOCK_DB_ROW_DATE,
               STATUS = Statuses.NEW,
               DESCRIPTION="Blue Ball",
               NAME="Blue Ball",
               PRICE="30"
            },
            new ProductModel{
               ID = new Guid("cbbbb5aa-b5cc-4ae3-9c96-4ab8b969db92"),
               CREATE_BY =  new Guid("00000000-0000-0000-0000-000000000000"),
               EDIT_BY =  new Guid("00000000-0000-0000-0000-000000000000"),
               CREATE_DATE = MOCK_DB_ROW_DATE,
               EDIT_DATE = MOCK_DB_ROW_DATE,
               STATUS = Statuses.NEW,
               DESCRIPTION="Purple Ball",
               NAME="Purple Ball",
               PRICE="30"
            },
            new ProductModel{
               ID = new Guid("dbbbb5aa-b5cc-4ae3-9c96-4ab8b969db92"),
               CREATE_BY =  new Guid("00000000-0000-0000-0000-000000000000"),
               EDIT_BY =  new Guid("00000000-0000-0000-0000-000000000000"),
               CREATE_DATE = MOCK_DB_ROW_DATE,
               EDIT_DATE = MOCK_DB_ROW_DATE,
               STATUS = Statuses.NEW,
               DESCRIPTION="PINK Ball",
               NAME="PINK Ball",
               PRICE="30"
            },
        };

      public static void PopulateProductsIfNotExist(IServiceProvider service)
      {
         using (var scope = service.CreateScope())
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
         foreach (var p in MOCK_PRODUCTS)
         {
            context.Products.Add(p);
         }

         var sresult = context.SaveChanges();
      }

   }
}
