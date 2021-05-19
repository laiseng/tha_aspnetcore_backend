using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using THA.Core.Authorization;
using THA.Entity.Main;
using THA.Model.Core;
using THA.Model.Employee;

namespace THA.DBInit
{
   public class PopulateEmployees
   {
      public static DateTime MOCK_DB_ROW_DATE = DateTime.Parse("2021-01-10T05:24:34.088Z");

      public static EmployeeModel[] MOCK_EMPLOYEES = new EmployeeModel[]
         {
               new EmployeeModel{
                  ID = new Guid("85bbb5aa-b5cc-4ae3-9c96-4ab8b969db95"),
                  CREATE_BY = new Guid("00000000-0000-0000-0000-000000000000"),
                  EDIT_BY =  new Guid("00000000-0000-0000-0000-000000000000"),
                  CREATE_DATE = MOCK_DB_ROW_DATE,
                  EDIT_DATE = MOCK_DB_ROW_DATE,
                 STATUS = Statuses.NEW,
                  FIRST_NAME = "John",
                  LAST_NAME="Doe",
                  EMPLOYEE_STATUS = EmployeeStatuses.Regular,
               },
               new EmployeeModel{
                  ID = new Guid("85bbb5aa-b5cc-4ae3-9c96-4ab8b969db96"),
                  CREATE_BY =  new Guid("00000000-0000-0000-0000-000000000000"),
                  EDIT_BY =  new Guid("00000000-0000-0000-0000-000000000000"),
                  CREATE_DATE = MOCK_DB_ROW_DATE,
                  EDIT_DATE = MOCK_DB_ROW_DATE,
                 STATUS = Statuses.NEW,
                  FIRST_NAME = "Jane",
                  LAST_NAME="Doe",
                  EMPLOYEE_STATUS = EmployeeStatuses.Regular
               },
               new EmployeeModel{
                  ID =new Guid("85bbb5aa-b5cc-4ae3-9c96-4ab8b969db97"),
                  CREATE_BY =  new Guid("00000000-0000-0000-0000-000000000000"),
                  EDIT_BY =  new Guid("00000000-0000-0000-0000-000000000000"),
                  CREATE_DATE = MOCK_DB_ROW_DATE,
                  EDIT_DATE = MOCK_DB_ROW_DATE,
                 STATUS = Statuses.NEW,
                  FIRST_NAME = "Harry",
                  LAST_NAME="Potter",
                  EMPLOYEE_STATUS = EmployeeStatuses.Regular
               },
         };

      public static void PopulateEmployeesIfNotExist(IServiceProvider service)
      {
         using (var scope = service.CreateScope())
         {
            var services = scope.ServiceProvider;
            try
            {
               var context = services.GetRequiredService<MainContext>();
               PopulateEmployees.Populate(context);
            }
            catch (Exception ex)
            {
               var logger = services.GetRequiredService<ILogger<MainContext>>();
               logger.LogError(ex, "Error when populating Employees");
            }
         }
      }

      public static void Populate(MainContext context)
      {
         context.Database.EnsureCreated();

         if (context.Employees.Any())
         {
            // DB has been populated nothing to do here
            return;
         }

         foreach (EmployeeModel e in MOCK_EMPLOYEES)
         {
            context.Employees.Add(e);
         }

         var sresult = context.SaveChanges();
      }
   }
}
