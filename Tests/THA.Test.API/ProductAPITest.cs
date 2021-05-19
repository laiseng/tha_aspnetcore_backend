using System;
using System.Threading.Tasks;
using Xunit;
using THA.Model.Account;
using THA_Api;
using THA.Model.User;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using System.Text.Json;
using System.Text;
using Serilog;
using Microsoft.Extensions.Configuration;
using THA.DBInit;
using THA.Model.Product;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using THA.Model.Core;

namespace THA.Test.API
{
   public class ProductAPITest
   {
      private readonly TestServer _server;

      public ProductAPITest()
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
         //_client = _server.CreateClient();

         //mock dummy data into in memory db
         PopulateUsers.PopulateUserIfNotExist(_server.Host.Services);
         PopulateProducts.PopulateProductsIfNotExist(_server.Host.Services);
      }
      async private Task<LoginResponseModel> GetJwtToken(LoginModel loginCredential)
      {
         var content = new StringContent(JsonSerializer.Serialize(loginCredential), Encoding.UTF8, "application/json");

         var response = await this._server.CreateClient().PostAsync("/api/Account", content);

         response.EnsureSuccessStatusCode();
         var responseString = await response.Content.ReadAsStringAsync();
         var responseBody = JsonSerializer.Deserialize<LoginResponseModel>(responseString);

         return responseBody;
      }

      async private Task<RequestBuilder> GenerateBearerAs(string uri, LoginModel loginCredential)
      {
         var tokenObject = await this.GetJwtToken(loginCredential);

         return _server.CreateRequest(uri)
            .AddHeader("Authorization", $"Bearer {tokenObject.token}");
      }

      [Fact]
      public async Task Put_Product()
      {
         //Arrange 
         var reqUserAs = new LoginModel { Email = "peter@tha.com", Password = "password" };

         var editedExistingProduct = PopulateProducts.MOCK_PRODUCTS[0];
         editedExistingProduct.NAME = "EDITE NAME";

         var content = new StringContent(JsonSerializer.Serialize(editedExistingProduct), Encoding.UTF8, "application/json");

         // Act
         var req = await this.GenerateBearerAs($"/api/product", reqUserAs);
         req.And(x => x.Content = content);

         var response = await req.SendAsync("PUT");


         response.EnsureSuccessStatusCode();
         var responseString = await response.Content.ReadAsStringAsync();
         var responseBody = JsonSerializer.Deserialize<ProductModel>(responseString);

         // Assert
         Assert.Equal(HttpStatusCode.OK, response.StatusCode);
         Assert.NotNull(responseString);
         Assert.NotNull(responseBody);
         Assert.Equal("EDITE NAME", responseBody.NAME);
      }

      [Fact]
      public async Task Put_Product_Conflict()
      {
         //Arrange 
         var reqUserAs = new LoginModel { Email = "peter@tha.com", Password = "password" };

         var editedExistingProduct = PopulateProducts.MOCK_PRODUCTS[1];
         editedExistingProduct.NAME = "EDITE NAME";
         // outdated edit date
         editedExistingProduct.EDIT_DATE = PopulateProducts.MOCK_DB_ROW_DATE.AddSeconds(-10);

         var content = new StringContent(JsonSerializer.Serialize(editedExistingProduct), Encoding.UTF8, "application/json");

         // Act
         var req = await this.GenerateBearerAs($"/api/product", reqUserAs);
         req.And(x => x.Content = content);

         var response = await req.SendAsync("PUT");

         // Assert
         Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
      }

      [Fact]
      public async Task Get_Product_Success()
      {

         //Arrange 
         var reqUserAs = new LoginModel { Email = "john@tha.com", Password = "password" };
         var productId = new Guid("bbbbb5aa-b5cc-4ae3-9c96-4ab8b969db90");
         // Act
         var req = await this.GenerateBearerAs($"/api/product/{productId}", reqUserAs);

         var response = await req.GetAsync();

         response.EnsureSuccessStatusCode();
         var responseString = await response.Content.ReadAsStringAsync();
         var responseBody = JsonSerializer.Deserialize<ProductModel>(responseString);
         // Assert
         Assert.Equal(HttpStatusCode.OK, response.StatusCode);
         Assert.NotNull(responseString);
         Assert.NotNull(responseBody);
         Assert.Equal(productId, responseBody.ID);


      }

      [Fact]
      public async Task Get_All_Success()
      {
         //Arrange 
         var reqUserAs = new LoginModel { Email = "john@tha.com", Password = "password" };

         // Act
         var req = await this.GenerateBearerAs($"/api/product/all", reqUserAs);

         var response = await req.GetAsync();
         response.EnsureSuccessStatusCode();
         var responseString = await response.Content.ReadAsStringAsync();
         var responseBody = JsonSerializer.Deserialize<List<ProductModel>>(responseString);

         // Assert
         Assert.Equal(HttpStatusCode.OK, response.StatusCode);
         Assert.NotNull(responseString);
         // response body should not contain deleted items
         Assert.False(responseBody.Exists(x => x.STATUS == Model.Core.Statuses.DELETE));
      }

      [Fact]
      public async Task Get_Product_Unauthorized()
      {

         //Arrange 
         var reqUserAs = new LoginModel { Email = "peter@tha.com", Password = "password" };
         var productId = new Guid("bbbbb5aa-b5cc-4ae3-9c96-4ab8b969db90");

         // Act
         var req = await this.GenerateBearerAs($"/api/product/{productId}", reqUserAs);
         var response = await req.GetAsync();

         // Assert
         Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
      }

      [Fact]
      public async Task Get_All_Unauthorized()
      {
         //Arrange 
         var reqUserAs = new LoginModel { Email = "peter@tha.com", Password = "password" };

         // Act
         var req = await this.GenerateBearerAs($"/api/product/all", reqUserAs);
         var response = await req.GetAsync();

         // Assert
         Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
      }

      [Fact]
      public async Task Add_New_Product()
      {
         //Arrange 
         var reqUserAs = new LoginModel { Email = "peter@tha.com", Password = "password" };
         var newProduct = new ProductModel
         {
            ID = new Guid("aaeeb5aa-b5cc-4ae3-9c96-4ab8b969db92"),
            CREATE_BY = new Guid("00000000-0000-0000-0000-000000000000"),
            EDIT_BY = new Guid("00000000-0000-0000-0000-000000000000"),
            CREATE_DATE = DateTime.UtcNow,
            EDIT_DATE = DateTime.UtcNow,
            STATUS = Statuses.NEW,
            DESCRIPTION = "NEW Blue Ball",
            NAME = "NEW Blue Ball",
            PRICE = "30"
         };

         var content = new StringContent(JsonSerializer.Serialize(newProduct), Encoding.UTF8, "application/json");

         // Act
         var req = await this.GenerateBearerAs($"/api/product", reqUserAs);
         req.And(x => x.Content = content);
         var response = await req.PostAsync();


         response.EnsureSuccessStatusCode();
         var responseString = await response.Content.ReadAsStringAsync();
         var responseBody = JsonSerializer.Deserialize<ProductModel>(responseString);

         // Assert
         Assert.Equal(HttpStatusCode.OK, response.StatusCode);
         Assert.NotNull(responseString);
         Assert.NotNull(responseBody);
      }

      [Fact]
      public async Task Delete_Product()
      {
         //Arrange 
         var reqUserAs = new LoginModel { Email = "peter@tha.com", Password = "password" };

         var deleteItem = PopulateProducts.MOCK_PRODUCTS[2];

         // Act
         var req = await this.GenerateBearerAs($"/api/product/{deleteItem.ID}", reqUserAs);

         var response = await req.SendAsync("DELETE");
         response.EnsureSuccessStatusCode();
         var responseString = await response.Content.ReadAsStringAsync();
         var responseBody = JsonSerializer.Deserialize<ProductModel>(responseString);

         // Assert
         Assert.Equal(HttpStatusCode.OK, response.StatusCode);
         Assert.NotNull(responseBody);
         Assert.Equal(Statuses.DELETE, responseBody.STATUS);
      }
   }
}
