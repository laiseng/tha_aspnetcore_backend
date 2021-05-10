using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using THA.Entity.Main;
using THA.Model.AppSettings;
using THA.Service.Product;

namespace THA_Api.Controllers
{
   [ApiController]
   [Route("api/[controller]/[action]")]
   public class PlaygroundController : ControllerBase
   {
      private static readonly string[] Summaries = new[]
      {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

      private readonly ILogger<PlaygroundController> _logger;
      private readonly IOptions<AppSetting> _appSetting;
      private readonly MainContext _mainContext;
      private readonly ProductRepository _productRepo;

      public PlaygroundController(ProductRepository productRepo, MainContext userContext, ILogger<PlaygroundController> logger, IOptions<AppSetting> appSetting)
      {
         _logger = logger;
         _mainContext = userContext;
         _appSetting = appSetting;
         _productRepo = productRepo;
      }


      [Authorize(policy: "GOD")]
      [HttpGet]
      public IActionResult god()
      {

         return new ObjectResult("you are god");
      }


      [Authorize(policy: "PRODUCT_ADMIN")]
      [HttpGet]
      public IActionResult productadmin()
      {

         return new ObjectResult("you are admin");
      }

      [Authorize(policy: "USER")]
      [HttpGet]
      public IActionResult user()
      {

         return new ObjectResult("you are USER");
         //return Utilities.HasAccess("28", Rights.PRODUCT_ADMIN);
      }

      [Authorize]
      [HttpGet]
      public IActionResult claims()
      {
         if (this.User.HasClaim(ClaimTypes.NameIdentifier, "3"))
         {
            _logger.LogInformation("===========it has it");
         }
         else
         {
            _logger.LogInformation("===========NOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO");
         }

         return new ObjectResult(this.User.Claims.Select(c => new { Type = c.Type, Value = c.Value }).ToList());
         //return Utilities.HasAccess("28", Rights.PRODUCT_ADMIN);
      }

      [HttpGet]
      [Route("{g}")]
      async public Task<IActionResult> gp(Guid g)
      {

         return new ObjectResult(g);

      }

   }
}
