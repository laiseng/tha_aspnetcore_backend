using System;
using THA.Model.Core;

namespace THA.Model.Base
{
   public interface IBaseModel
   {
      int CREATE_BY { get; set; }
      DateTime CREATE_DATE { get; set; }
      int EDIT_BY { get; set; }
      DateTime EDIT_DATE { get; set; }
      int ID { get; set; }
      Statuses STATUS { get; set; }
   }
}