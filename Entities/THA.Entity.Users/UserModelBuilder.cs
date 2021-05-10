using Microsoft.EntityFrameworkCore;
using THA.Model.User;


namespace THA.Entity.Users
{
   public class UserModelBuilder : DbContext
   {
      public static void BuildModel(ModelBuilder modelBuilder)
      {
         modelBuilder.Entity<UserModel>().ToTable("USERS");
      }
   }
}