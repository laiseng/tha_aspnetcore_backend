using Microsoft.AspNetCore.Http;
using System;
using System.Data;
using System.Threading.Tasks;
using THA.Entity.Main;
using THA.Model.Product;
using THA.Service.Base;

namespace THA.Service.Product
{
   public class ProductRepository : AbstractRepository<ProductModel, MainContext>, IProductRepository
   {
      public ProductRepository(MainContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
      {


      }

      /// <summary>
      /// Update product item
      /// </summary>
      /// <param name="entity">product item to update</param>
      /// <returns>returns updated product item or null if none updated</returns>
      public override async Task<ProductModel> Update(ProductModel entity)
      {
         if (entity == null) throw new ArgumentNullException($"{nameof(Update)} entity must not be null");

         var found = await this.Get(entity.ID);
         if (found != null)
         {
            if (found.EDIT_DATE != entity.EDIT_DATE) throw new DBConcurrencyException("Update failed, entity you tries to update is outdated, refresh and retry");

            found.NAME = entity.NAME;
            found.DESCRIPTION = entity.DESCRIPTION;
            found.PRICE = entity.PRICE;
            var updatedEntity = await base.Update(found);

            return updatedEntity;

         }
         else
         {
            return null;
         }
      }
   }
}
