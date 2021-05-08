using System;
using THA.Model.Core;

namespace THA.Model.Base
{
   public class BaseModal : IBaseModel
   {
      public int ID { get; set; }
      public int CREATE_BY { get; set; }
      public int EDIT_BY { get; set; }
      public DateTime CREATE_DATE { get; set; }
      public DateTime EDIT_DATE { get; set; }
      public Statuses STATUS { get; set; }

   }
}
