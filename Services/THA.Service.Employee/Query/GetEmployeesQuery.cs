using MediatR;
using System;
using System.Collections.Generic;
using THA.Model.Employee;

namespace THA.Service.Employee
{
   public class GetEmployeesQuery : IRequest<List<EmployeeModel>>
   {
   }
}
