namespace CostControl.Data.EntityConfiguration.CostControl
{
    using Data.EntityConfiguration.Base;
    using Entity.Models.CostControl;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SalePointConfigure : BaseNamedEntityConfigure<SalePoint, long>
    {
        public override void Configure(EntityTypeBuilder<SalePoint> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(e => e.Code)
                .HasMaxLength(10)
                .IsRequired();

            entityTypeBuilder
                .HasIndex(e => e.Code)
                .IsUnique();

            entityTypeBuilder
                .Property(e => e.EnglishName)
                .HasMaxLength(250)
                .IsRequired();

            entityTypeBuilder
                .HasIndex(e => e.EnglishName)
                .IsUnique();

            entityTypeBuilder
                .Property(e => e.IsHall)
                .HasDefaultValue(false)
                .HasDefaultValueSql("0");

            entityTypeBuilder
               .ToTable("SalePoint", "dbo");
        }
    }
}