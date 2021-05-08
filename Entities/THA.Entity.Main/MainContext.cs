using Microsoft.EntityFrameworkCore;
using THA.Entity.Products;
using THA.Entity.Users;
using THA.Model.Product;
using THA.Model.User;

namespace THA.Entity.Main
{
   public class MainContext : DbContext
   {
      public DbSet<UserModel> Users { get; set; }
      public DbSet<ProductModel> Products { get; set; }
      public MainContext(DbContextOptions options) : base(options) { }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         UserModelBuilder.BuildModel(modelBuilder);
         ProductModelBuilder.BuildModel(modelBuilder);
      }
   }
}
