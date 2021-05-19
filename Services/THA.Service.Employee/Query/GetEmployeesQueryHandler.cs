using System.Threading;
using System.Threading.Tasks;
using MediatR;
using THA.Model.Employee;
using System.Collections.Generic;

namespace THA.Service.Employee
{

   public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<EmployeeModel>>
   {
      private readonly IEmployeeRepository _employeeRepository;

      public GetEmployeesQueryHandler(IEmployeeRepository employeeRepository)
      {
         _employeeRepository = employeeRepository;
      }

      public async Task<List<EmployeeModel>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
      {
         return await _employeeRepository.GetAll();
      }

   }
}