using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using THA.Model.AppSettings;
using THA.Model.Product;
using THA.Service.Product;

namespace THA_Api.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class ProductController : ControllerBase
   {
      private readonly ILogger<ProductController> _logger;
      private readonly IProductRepository _productRepository;


      public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
      {
         _logger = logger;
         _productRepository = productRepository;
      }

      [Authorize(policy: "USER")]
      [HttpGet]
      [Route("{productId}")]
      [ProducesResponseType(200)]
      [ProducesResponseType(404)]
      async public Task<ActionResult<ProductModel>> GetProduct(Guid productId)
      {
         var found = await _productRepository.Get(productId);
         return found != null ? found : NotFound();
      }

      [Authorize(policy: "USER")]
      [HttpGet]
      [Route("all")]
      [ProducesResponseType(200)]
      [ProducesResponseType(404)]
      async public Task<ActionResult<List<ProductModel>>> GetAll()
      {
         var found = await this._productRepository.GetAll();
         return found != null ? found : NotFound();
      }


      [Authorize(policy: "PRODUCT_ADMIN")]
      [HttpPost]
      [Route("")]
      [ProducesResponseType(201)]
      [ProducesResponseType(404)]
      async public Task<ActionResult<ProductModel>> AddProduct([FromBody] ProductModel product)
      {
         if (!ModelState.IsValid) return BadRequest(ModelState);

         var added = await this._productRepository.Add(product);

         return added != null ? added : NotFound("Failed to add Product");
      }


      [Authorize(policy: "PRODUCT_ADMIN")]
      [HttpPut]
      [Route("")]
      [ProducesResponseType(200)]
      [ProducesResponseType(404)]
      [ProducesResponseType(409)]
      async public Task<ActionResult<ProductModel>> UpdateProduct([FromBody] ProductModel product)
      {
         try
         {

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updatedEntity = await this._productRepository.Update(product);

            return updatedEntity != null ? updatedEntity : NotFound("Update failed: item not found in db");

         }
         catch (DBConcurrencyException)
         {
            this._logger.LogError("Attempt to update product with outdated entity");
            return Conflict("Failed to update Product with outdated info. refresh and retry update");
         }
      }


      [Authorize(policy: "PRODUCT_ADMIN")]
      [HttpDelete]
      [Route("{productId}")]
      [ProducesResponseType(204)]
      [ProducesResponseType(404)]
      async public Task<ActionResult<ProductModel>> Delete(Guid productId)
      {
         var deleted = await this._productRepository.Delete(productId);
         return deleted != null ? deleted : NotFound($"Failed to delete productId : {productId}");
      }

   }
}
