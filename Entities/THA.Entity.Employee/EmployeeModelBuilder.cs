using Microsoft.EntityFrameworkCore;
using System;
using THA.Model.Employee;

namespace THA.Entity.Employee
{
   public class EmployeeModelBuilder : DbContext
   {
      public static void BuildModel(ModelBuilder modelBuilder)
      {
         modelBuilder.Entity<EmployeeModel>().ToTable("EMPLOYEES");
      }
   }
}
