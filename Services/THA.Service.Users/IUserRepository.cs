using System.Threading.Tasks;
using THA.Entity.Main;
using THA.Model.Account;
using THA.Model.User;
using THA.Service.Base;

namespace THA.Service.Users
{
   public interface IUserRepository : IRepositoryBase<UserModel>
   {
      Task<UserModel> ValidateLogin(LoginModel login);
   }
}