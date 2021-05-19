using System.Threading.Tasks;
using THA.Model.Employee;
using THA.Model.Product;
using THA.Service.Base;

namespace THA.Service.Employee
{
   public interface IEmployeeRepository : IRepositoryBase<EmployeeModel>
   {
   }
}