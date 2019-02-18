using CostControl.Entity.Models.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CostControl.Data.EntityConfiguration.Base
{
    public abstract class BaseNamedEntityConfigure<TEntity, TKey> :
        BaseEntityConfigure<TEntity, TKey> where TEntity : SupperNameEntity<TKey>
    {
        public override void Configure(EntityTypeBuilder<TEntity> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(e => e.Name)
                .IsRequired()
                .IsUnicode();

            //entityTypeBuilder
            //    .HasBaseType<BaseEntity>();
        }
    }
}