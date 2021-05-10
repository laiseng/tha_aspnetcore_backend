using System.Threading.Tasks;
using THA.Model.Product;
using THA.Service.Base;

namespace THA.Service.Product
{
   public interface IProductRepository : IRepositoryBase<ProductModel>
   {
      Task<ProductModel> Update(ProductModel entity);
   }
}