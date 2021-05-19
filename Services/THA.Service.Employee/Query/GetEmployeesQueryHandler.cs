using System.Threading;
using System.Threading.Tasks;
using MediatR;
//using OrderApi.Data.Repository.v1;
//using OrderApi.Domain;
using THA.Model.Employee;
using System.Collections.Generic;

namespace THA.Service.Employee
{

   public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<EmployeeModel>>
   {
      private readonly IEmployeeRepository _employeeRepository;

      public GetEmployeesQueryHandler(IEmployeeRepository orderRepository)
      {
         _employeeRepository = orderRepository;
      }

      public async Task<List<EmployeeModel>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
      {
         return await _employeeRepository.GetAll();
      }

   }
}