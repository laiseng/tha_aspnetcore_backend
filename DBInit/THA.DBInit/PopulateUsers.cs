using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using THA.Core.Authorization;
using THA.Entity.Main;
using THA.Model.Core;
using THA.Model.User;

namespace THA.DBInit
{
   public class PopulateUsers
   {
      public static void PopulateUserIfNotExist(IHost host)
      {
         using (var scope = host.Services.CreateScope())
         {
            var services = scope.ServiceProvider;
            try
            {
               var context = services.GetRequiredService<MainContext>();
               PopulateUsers.Populate(context);
            }
            catch (Exception ex)
            {
               var logger = services.GetRequiredService<ILogger<MainContext>>();
               logger.LogError(ex, "Error when populating Users");
            }
         }

      }

      public static void Populate(MainContext context)
      {
         context.Database.EnsureCreated();

         if (context.Users.Any())
         {
            // DB has been populated nothing to do here
            return;
         }

         var users = new UserModel[]
         {
            new UserModel{
               ID = 1,
               CREATE_BY = 0,
               EDIT_BY = 0,
               CREATE_DATE = DateTime.UtcNow,
               EDIT_DATE = DateTime.UtcNow,
               STATUS = Statuses.NEW,
               ROLE=Roles.GOD,
               NAME = "god",
               EMAIL = "god@tha.com",
               PASSWORD_HASH = Utilities.GetSha256Hash("password")
            },
            new UserModel{
               ID = 2,
               CREATE_BY = 0,
               EDIT_BY = 0,
               CREATE_DATE = DateTime.UtcNow,
               EDIT_DATE = DateTime.UtcNow,
               STATUS = Statuses.NEW,
               ROLE = Roles.PRODUCT_ADMIN,
               NAME = "Peter",
               EMAIL = "peter@tha.com",
               PASSWORD_HASH = Utilities.GetSha256Hash("password")
            },
            new UserModel{
               ID = 3,
               CREATE_BY = 0,
               EDIT_BY = 0,
               CREATE_DATE = DateTime.UtcNow,
               EDIT_DATE = DateTime.UtcNow,
               STATUS = Statuses.NEW,
               ROLE = Roles.USER,
               NAME = "John",
               EMAIL = "john@tha.com",
               PASSWORD_HASH = Utilities.GetSha256Hash("password")
            },
            new UserModel{
               ID = 4,
               CREATE_BY = 0,
               EDIT_BY = 0,
               CREATE_DATE = DateTime.UtcNow,
               EDIT_DATE = DateTime.UtcNow,
               STATUS = Statuses.NEW,
               ROLE = Roles.USER,
               NAME = "derp",
               EMAIL = "derp@tha.com",
               PASSWORD_HASH = Utilities.GetSha256Hash("password")
            },
         };

         foreach (UserModel u in users)
         {
            context.Users.Add(u);
         }

         var sresult = context.SaveChanges();
      }
   }
}
