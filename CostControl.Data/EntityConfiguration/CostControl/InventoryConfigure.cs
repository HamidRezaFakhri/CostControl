using CostControl.Data.EntityConfiguration.Base;
using CostControl.Entity.Models.CostControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CostControl.Data.EntityConfiguration.CostControl
{
    public class InventoryConfigure : BaseNamedEntityConfigure<Inventory, long>
    {
        public override void Configure(EntityTypeBuilder<Inventory> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .HasIndex(e => e.Name)
                .IsUnique();

            entityTypeBuilder
               .ToTable("Inventory", "dbo");
        }
    }
}