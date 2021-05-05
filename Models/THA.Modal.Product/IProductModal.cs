using System;
using THA.Modal.Base;

namespace THA.Modal.Product
{
   public interface IProductModal : IBaseModal
   {
      public string NAME { get; set; }
      public string PRICE { get; set; }
      public string DESCRIPTION { get; set; }
   }
}
