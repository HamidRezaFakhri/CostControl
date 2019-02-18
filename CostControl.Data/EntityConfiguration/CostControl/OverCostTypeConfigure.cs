using CostControl.Data.EntityConfiguration.Base;
using CostControl.Entity.Models.CostControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CostControl.Data.EntityConfiguration.CostControl
{
    public class OverCostTypeConfigure : BaseNamedEntityConfigure<OverCostType, byte>
    {
        public override void Configure(EntityTypeBuilder<OverCostType> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .HasIndex(e => e.Name)
                .IsUnique();

            entityTypeBuilder
               .ToTable("OverCostType", "dbo");
        }
    }
}