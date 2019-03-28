namespace CostControl.Data.EntityConfiguration.Base
{
    using Entity.Models.Base;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public abstract class BaseNamedEntityConfigure<TEntity, TKey> :
        BaseEntityConfigure<TEntity, TKey> where TEntity : SupperNameEntity<TKey>
    {
        public override void Configure(EntityTypeBuilder<TEntity> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(e => e.Name)
                .HasMaxLength(250)
                .HasColumnType("NVARCHAR(250)")
                .IsRequired()
                .IsUnicode();

            entityTypeBuilder
                .HasIndex(e => e.Name)
                .IsUnique();

            //entityTypeBuilder
            //    .HasBaseType<BaseEntity>();
        }
    }
}