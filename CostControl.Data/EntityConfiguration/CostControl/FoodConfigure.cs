namespace CostControl.Data.EntityConfiguration.CostControl
{
    using Data.EntityConfiguration.Base;
    using Entity.Models.CostControl;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class FoodConfigure : BaseNamedEntityConfigure<Food, long>
    {
        public override void Configure(EntityTypeBuilder<Food> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(e => e.SaleCostPointId)
                .IsRequired();

            entityTypeBuilder
                .HasIndex(e => e.Code)
                .IsUnique();

            entityTypeBuilder
                .Property(e => e.Code)
                .HasMaxLength(10);

            entityTypeBuilder
                .Property(e => e.EnglishName)
                .HasMaxLength(250)
                .IsRequired();

            entityTypeBuilder
                .Property(e => e.Price)
                .HasColumnType("numeric(28,2)")
                .IsRequired();

            entityTypeBuilder
               .ToTable("Food", "dbo");
        }
    }
}