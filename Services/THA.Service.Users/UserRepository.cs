using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using THA.Core.Authorization;
using THA.Entity.Main;
using THA.Model.Account;
using THA.Model.User;
using THA.Service.Product;


namespace THA.Service.Users
{
   public class UserRepository : AbstractRepository<UserModel, MainContext>, IUserRepository
   {
      private readonly MainContext _mainContext;
      public UserRepository() { }
      public UserRepository(MainContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
      {
         this._mainContext = context;
      }

      /// <summary>
      /// Login with email and password
      /// </summary>
      /// <param name="login">login email and passsword</param>
      /// <returns>return valid logged in user</returns>
      async public Task<UserModel> ValidateLogin(LoginModel login)
      {
         try
         {
            var user = this._mainContext.Users.Where(u => u.EMAIL.ToUpper() == login.Email.ToUpper()
                                                          && u.PASSWORD_HASH == Utilities.GetSha256Hash(login.Password));
            if (await user.AnyAsync()) return await user.FirstAsync();

            return null;

         }
         catch (Exception ex)
         {
            throw new Exception($"{nameof(ValidateLogin)} Login failed with message - {ex.Message}");
         }

      }
   }
}
