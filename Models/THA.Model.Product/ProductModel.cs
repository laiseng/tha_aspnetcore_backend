using System;
using THA.Model.Base;

namespace THA.Model.Product
{
   public class ProductModel : BaseModal
   {
      public string NAME { get; set; }
      public string PRICE { get; set; }
      public string DESCRIPTION { get; set; }
   }
}
