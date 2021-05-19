using System;
using THA.Model.Base;

namespace THA.Model.Employee
{

   public enum EmployeeStatuses
   {
      Regular = 0,
      Contractor = 1
   }

   public class EmployeeModel : BaseModal
   {
      public string FIRST_NAME { get; set; }
      public string LAST_NAME { get; set; }
      public EmployeeStatuses EMPLOYEE_STATUS { get; set; }

   }
}