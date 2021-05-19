using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using THA.Model.Employee;
using THA_Api.Controllers;
using THA.Service.Employee;
using Xunit;
using Microsoft.Extensions.Logging;
using THA.DBInit;

namespace THA.Test.API
{
   public class EmployeeAPITests
   {
      private readonly EmployeeController _tester;
      private readonly IMediator _mediator;
      private readonly ILogger<EmployeeController> _logger;

      public EmployeeAPITests()
      {
         _mediator = A.Fake<IMediator>();
         _logger = A.Fake<ILogger<EmployeeController>>();
         _tester = new EmployeeController(_logger, _mediator);
      }


      [Fact]
      public async void Get_All_Employees_Found()
      {
         A.CallTo(() => _mediator.Send(A<GetEmployeesQuery>._, A<CancellationToken>._)).Returns(new List<EmployeeModel>(PopulateEmployees.MOCK_EMPLOYEES));

         var result = await _tester.GetAllCQRS();
         Assert.Equal(result.Value, new List<EmployeeModel>(PopulateEmployees.MOCK_EMPLOYEES));
      }


      [Fact]
      public async void Get_All_Employees_404Found()
      {
         A.CallTo(() => _mediator.Send(A<GetEmployeesQuery>._, A<CancellationToken>._)).Returns(new List<EmployeeModel>());
         var result = await _tester.GetAllCQRS();
         Assert.Equal((int)HttpStatusCode.NotFound, (result.Result as StatusCodeResult)?.StatusCode);
      }

   }
}