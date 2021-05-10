using System.Security.Cryptography;
using System.Text;

namespace THA.Core.Authorization
{
   public class Utilities
   {
      public static bool HasAccess(int accessRight, Rights right)
      {
         return (accessRight & (int)right) == (int)right;
      }

      public static int AddAccess(int accessRight, params Rights[] rights)
      {
         foreach (var right in rights)
         {

            accessRight = accessRight | (int)right;
         }

         return accessRight;
      }

      public static int RemoveAccess(int accessRight, Rights right)
      {
         return accessRight ^ (int)right;
      }

      public static string GetSha256Hash(string rawData)
      {
         using (SHA256 sha256Hash = SHA256.Create())
         {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
               builder.Append(bytes[i].ToString("x2"));
            }

            return builder.ToString();
         }
      }
   }
}
