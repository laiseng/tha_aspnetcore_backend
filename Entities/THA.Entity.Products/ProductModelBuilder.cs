using Microsoft.EntityFrameworkCore;
using THA.Model.Product;

namespace THA.Entity.Products
{

   public class ProductModelBuilder : DbContext
   {
      public static void BuildModel(ModelBuilder modelBuilder)
      {
         modelBuilder.Entity<ProductModel>().ToTable("PRODUCTS");
      }

   }

}
