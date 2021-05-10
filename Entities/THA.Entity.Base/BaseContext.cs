using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using THA.Model.Base;

namespace THA.Entity.Base
{
   public class BaseContext<T> where T : BaseModal
   {
      protected static EntityTypeBuilder<T> BaseModelEntityProperties(EntityTypeBuilder<T> entity, string privateKeyName)
      {
         entity.HasKey(e => e.ID).HasName(privateKeyName);
         entity.Property(e => e.ID).HasColumnName("ID");
         entity.Property(e => e.CREATE_DATE).HasColumnName("CREATEDATE");
         entity.Property(e => e.EDIT_DATE).HasColumnName("EDITDATE");
         entity.Property(e => e.STATUS).HasColumnName("STATUS");
         return entity;
      }
   }
}
