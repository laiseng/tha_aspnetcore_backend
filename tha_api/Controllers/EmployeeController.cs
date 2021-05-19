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
      private readonly IMediator _mediator;

      public EmployeeController(ILogger<EmployeeController> logger, IMediator mediator)
      {
         _logger = logger;
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
            return all != null && all.Count > 0 ? all : NotFound();
         }
         catch (Exception ex)
         {
            this._logger.LogError($"Exception: {ex.Message}");
            return BadRequest(ex.Message);
         }
      }
   }
}
