namespace CostControl.Data.EntityConfiguration.CostControl
{
    using Data.EntityConfiguration.Base;
    using Entity.Models.CostControl;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class InventoryConfigure : BaseNamedEntityConfigure<Inventory, long>
    {
        public override void Configure(EntityTypeBuilder<Inventory> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(e => e.Code)
                .HasMaxLength(10);

            entityTypeBuilder
                .HasIndex(e => e.Code)
                .IsUnique();

            entityTypeBuilder
                .Property(e => e.IsWasted)
                .HasDefaultValue(false)
                .HasDefaultValueSql("0");

            entityTypeBuilder
               .ToTable("Inventory", "dbo");
        }
    }
}