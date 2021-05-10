using System;
using System.Threading.Tasks;
using Xunit;
using THA.Model.Account;
using THA_Api;
using THA_Api.Controllers;
using THA.Service.Users;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using THA.Model.AppSettings;
using Microsoft.AspNetCore.Mvc;
using THA.Model.User;
using THA.Model.Core;
using THA.Core.Authorization;
using Microsoft.AspNetCore.Http;
using THA.Entity.Main;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using System.Text.Json;
using System.Text;
using Serilog;
using Microsoft.Extensions.Configuration;
using THA.DBInit;
using System.Net;

namespace THA.Test.API
{
   public class AccountAPITest
   {
      private readonly TestServer _server;
      private readonly HttpClient _client;

      public AccountAPITest()
      {
         // Arrange
         var host = new WebHostBuilder()
            .UseEnvironment("Development")
            .UseConfiguration(
               new ConfigurationBuilder().AddJsonFile("appsettings.development.json").Build()
            )
            .UseSerilog()
            .UseStartup<Startup>();
         _server = new TestServer(host);
         _client = _server.CreateClient();

         //mock dummy data into in memory db
         PopulateUsers.PopulateUserIfNotExist(_server.Host.Services);
         PopulateProducts.PopulateProductsIfNotExist(_server.Host.Services);
      }

      [Fact]
      public async Task Login_Success()
      {
         //Arrange 
         var loginReq = new LoginModel()
         {
            Email = "john@tha.com",
            Password = "password"
         };
         var content = new StringContent(JsonSerializer.Serialize(loginReq), Encoding.UTF8, "application/json");

         // Act
         var response = await _client.PostAsync("/api/Account", content);

         response.EnsureSuccessStatusCode();
         var responseString = await response.Content.ReadAsStringAsync();
         var responseBody = JsonSerializer.Deserialize<LoginResponseModel>(responseString);
         // Assert
         Assert.Equal(HttpStatusCode.Created, response.StatusCode);
         Assert.NotNull(responseString);
         Assert.NotNull(responseBody.token);
      }

      [Fact]
      public async Task Login_Failed()
      {
         //Arrange 
         var loginReq = new LoginModel()
         {
            Email = "usernotexist@EM.com",
            Password = "password"
         };
         var content = new StringContent(JsonSerializer.Serialize(loginReq), Encoding.UTF8, "application/json");

         // Act
         var response = await _client.PostAsync("/api/Account", content);

         // Assert
         Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
      }
   }
}
