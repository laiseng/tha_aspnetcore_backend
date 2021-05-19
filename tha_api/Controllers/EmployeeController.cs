using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using THA.Model.AppSettings;
using THA.Model.Employee;
using THA.Service.Base;
using THA.Service.Employee;

namespace THA_Api.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class EmployeeController : ControllerBase
   {
      private readonly ILogger<EmployeeController> _logger;
      private readonly IEmployeeRepository _employeeRepository;
      private readonly IMediator _mediator;

      public EmployeeController(IEmployeeRepository employeeRepository, ILogger<EmployeeController> logger, IMediator mediator)
      {
         _logger = logger;
         _employeeRepository = employeeRepository;
         _mediator = mediator;
      }

      [AllowAnonymous]
      [HttpGet]
      [Route("all")]
      [ProducesResponseType(200)]
      [ProducesResponseType(404)]
      async public Task<ActionResult<List<EmployeeModel>>> GetAllCQRS()
      {
         try
         {
            var all = await this._mediator.Send(new GetEmployeesQuery());
            return all != null ? all : NotFound();

         }
         catch (Exception ex)
         {
            return BadRequest(ex.Message);
         }
      }
   }
}
