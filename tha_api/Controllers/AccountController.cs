using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using THA.Model.Account;
using THA.Model.AppSettings;
using THA.Model.User;
using THA.Service.Users;

namespace THA_Api.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class AccountController : ControllerBase
   {

      private readonly ILogger<AccountController> _logger;
      private readonly IUserRepository _userRepository;
      private readonly IOptions<AppSetting> _appSetting;

      public AccountController(IUserRepository userRepository, ILogger<AccountController> logger, IOptions<AppSetting> appSetting)
      {
         _logger = logger;
         _appSetting = appSetting;
         _userRepository = userRepository;
      }


      [AllowAnonymous]
      [HttpPost]
      [ProducesResponseType(201)]
      [ProducesResponseType(401)]
      async public Task<ActionResult<LoginResponseModel>> Login([FromBody] LoginModel login)
      {
         this._logger.LogInformation($"whose logging in {JsonSerializer.Serialize(login)}");
         var userFound = await this._userRepository.ValidateLogin(login);
         if (userFound != null)
         {
            var tokenString = this.CreateJWT(userFound);
            return StatusCode(201, new LoginResponseModel { token = tokenString });
         }

         return Unauthorized();
      }

      [Authorize]
      [HttpDelete]
      public IActionResult Logout()
      {
         // TODO: cleaning all services session base on jti
         return StatusCode(204);
      }

      private string CreateJWT(UserModel userInfo)
      {
         var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSetting.Value.Jwt.Key));
         var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

         var claims = new List<Claim>() {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, userInfo.ID.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, userInfo.EMAIL),
            new Claim(ClaimTypes.Role, userInfo.ROLE.ToString()),
         };

         var token = new JwtSecurityToken(
              _appSetting.Value.Jwt.Issuer,
              _appSetting.Value.Jwt.Issuer,
              claims,
              expires: DateTime.Now.AddMinutes(128),
              signingCredentials: credentials);

         return new JwtSecurityTokenHandler().WriteToken(token);
      }


   }
}
