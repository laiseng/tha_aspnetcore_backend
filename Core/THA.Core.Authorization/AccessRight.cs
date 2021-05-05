using System;

namespace THA.Core.Authorization
{
   public class AccessRight : IAccessRight
   {
      public bool HasAccess(int accessRight, Rights right)
      {
         return (accessRight & (int)right) == (int)right;
      }

      public int AddAccess(int accessRight, Rights right)
      {
         return accessRight | (int)right;
      }

      public int RemoveAccess(int accessRight, Rights right)
      {
         return accessRight ^ (int)right;
      }
   }
}
