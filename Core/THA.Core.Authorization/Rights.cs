using System;
using System.Collections.Generic;
using System.Text;

namespace THA.Core.Authorization
{
   /// <summary>
   /// Access Rights enum value should follow x^2 format
   /// </summary>
   public enum Rights
   {
      GOD = int.MaxValue,
      CREATE_USER = 2,
      DELETE_USER = 4,
      PRODUCT_ADMIN = 8,
      PRODUCT_DELETE = 16,
      PRODUCT_READ_ALL = 32,
   }
}
