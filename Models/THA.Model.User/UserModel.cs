using THA.Model.Base;

namespace THA.Model.User
{
   public enum Roles
   {
      GOD,
      PRODUCT_ADMIN,
      USER
   }
   public class UserModel : BaseModal
   {
      public string NAME { get; set; }
      public string EMAIL { get; set; }
      public string PASSWORD_HASH { get; set; }
      //public int ACCESS_RIGHTS { get; set; }
      public Roles ROLE { get; set; }

   }
}
