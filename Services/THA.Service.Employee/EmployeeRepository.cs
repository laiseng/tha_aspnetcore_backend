using Microsoft.AspNetCore.Http;
using System;
using System.Data;
using System.Threading.Tasks;
using THA.Entity.Main;
using THA.Model.Employee;
using THA.Model.Product;
using THA.Service.Base;

namespace THA.Service.Employee
{
   public class EmployeeRepository : AbstractRepository<EmployeeModel, MainContext>, IEmployeeRepository
   {
      public EmployeeRepository(MainContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
      {


      }
   }
}
