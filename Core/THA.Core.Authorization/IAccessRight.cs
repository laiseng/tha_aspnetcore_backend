namespace THA.Core.Authorization
{
   public interface IAccessRight
   {
      int AddAccess(int accessRight, Rights right);
      bool HasAccess(int accessRight, Rights right);
      int RemoveAccess(int accessRight, Rights right);
   }
}