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
      public static DateTime MOCK_DB_ROW_DATE = DateTime.Parse("2021-01-10T05:24:34.088Z");
      public static UserModel[] MOCK_USERS = new UserModel[]
         {
            new UserModel{
               ID = new Guid("85bbb5aa-b5cc-4ae3-9c96-4ab8b969db95"),
               CREATE_BY = new Guid("00000000-0000-0000-0000-000000000000"),
               EDIT_BY =  new Guid("00000000-0000-0000-0000-000000000000"),
               CREATE_DATE = MOCK_DB_ROW_DATE,
               EDIT_DATE = MOCK_DB_ROW_DATE,
               STATUS = Statuses.NEW,
               ROLE=Roles.GOD,
               NAME = "god",
               EMAIL = "god@tha.com",
               PASSWORD_HASH = Utilities.GetSha256Hash("password")
            },
            new UserModel{
               ID = new Guid("85bbb5aa-b5cc-4ae3-9c96-4ab8b969db96"),
               CREATE_BY =  new Guid("00000000-0000-0000-0000-000000000000"),
               EDIT_BY =  new Guid("00000000-0000-0000-0000-000000000000"),
               CREATE_DATE = MOCK_DB_ROW_DATE,
               EDIT_DATE = MOCK_DB_ROW_DATE,
               STATUS = Statuses.NEW,
               ROLE = Roles.PRODUCT_ADMIN,
               NAME = "Peter",
               EMAIL = "peter@tha.com",
               PASSWORD_HASH = Utilities.GetSha256Hash("password")
            },
            new UserModel{
               ID =new Guid("85bbb5aa-b5cc-4ae3-9c96-4ab8b969db97"),
               CREATE_BY =  new Guid("00000000-0000-0000-0000-000000000000"),
               EDIT_BY =  new Guid("00000000-0000-0000-0000-000000000000"),
               CREATE_DATE = MOCK_DB_ROW_DATE,
               EDIT_DATE = MOCK_DB_ROW_DATE,
               STATUS = Statuses.NEW,
               ROLE = Roles.USER,
               NAME = "John",
               EMAIL = "john@tha.com",
               PASSWORD_HASH = Utilities.GetSha256Hash("password")
            },
            new UserModel{
               ID = new Guid("85bbb5aa-b5cc-4ae3-9c96-4ab8b969db98"),
               CREATE_BY =  new Guid("00000000-0000-0000-0000-000000000000"),
               EDIT_BY =  new Guid("00000000-0000-0000-0000-000000000000"),
               CREATE_DATE = MOCK_DB_ROW_DATE,
               EDIT_DATE = MOCK_DB_ROW_DATE,
               STATUS = Statuses.NEW,
               ROLE = Roles.USER,
               NAME = "derp",
               EMAIL = "derp@tha.com",
               PASSWORD_HASH = Utilities.GetSha256Hash("password")
            },
         };

      public static void PopulateUserIfNotExist(IServiceProvider service)
      {
         using (var scope = service.CreateScope())
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

         foreach (UserModel u in MOCK_USERS)
         {
            context.Users.Add(u);
         }

         var sresult = context.SaveChanges();
      }
   }
}
