using System;
using THA.Modal.Core;

namespace THA.Modal.Base
{
   public interface IBaseModal
   {
      public int ID { get; set; }
      public int CREATE_BY { get; set; }
      public int EDIT_BY { get; set; }
      public DateTime CREATE_DATE { get; set; }
      public DateTime EDIT_DATE { get; set; }
      public Statuses STATUS { get; set; }

   }
}
