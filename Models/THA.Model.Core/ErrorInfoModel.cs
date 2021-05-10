using System;
using System.Text.Json;
namespace THA.Model.Core
{
   public class ErrorInfoModel
   {
      public Guid ErrorId { get; set; }
      public string SessionId { get; set; }
      public int StatusCode { get; set; }
      public string Message { get; set; }


      public override string ToString()
      {
         return JsonSerializer.Serialize(this);
      }
   }
}
