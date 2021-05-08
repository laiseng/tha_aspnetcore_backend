using System.Collections.Generic;
using System.Threading.Tasks;
using THA.Model.Base;

namespace THA.Service.Base
{
   public interface IRepositoryBase<T> where T : class, IBaseModel
   {
      Task<List<T>> GetAll();
      Task<T> Get(int id);
      Task<T> Add(T entity);
      Task<T> Update(T entity);
      Task<T> Delete(int id);

   }
}
