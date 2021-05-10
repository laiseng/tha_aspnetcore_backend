using System;
using THA.Model.Core;

namespace THA.Model.Base
{
   public interface IBaseModel
   {
      Guid ID { get; set; }
      Guid CREATE_BY { get; set; }
      DateTime CREATE_DATE { get; set; }
      Guid EDIT_BY { get; set; }
      DateTime EDIT_DATE { get; set; }
      Statuses STATUS { get; set; }
   }
}