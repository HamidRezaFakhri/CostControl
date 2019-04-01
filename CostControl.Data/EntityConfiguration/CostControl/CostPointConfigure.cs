namespace CostControl.Data.EntityConfiguration.CostControl
{
    using Data.EntityConfiguration.Base;
    using Entity.Models.CostControl;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CostPointConfigure : BaseEntityConfigure<CostPoint, long>
    {
        public override void Configure(EntityTypeBuilder<CostPoint> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(e => e.Name)
                .HasMaxLength(250)
                .HasColumnType("NVARCHAR(250)")
                .IsRequired()
                .IsUnicode();

            entityTypeBuilder
                .HasIndex(e => e.Code)
                .IsUnique();

            entityTypeBuilder
                .Property(e => e.Code)
                .HasMaxLength(10);

            entityTypeBuilder
                .HasIndex(e => new { e.CostPointGroupId, e.Name })
                .IsUnique();

            entityTypeBuilder
               .ToTable("CostPoint", "dbo");
        }
    }
}