using CostControl.Data.EntityConfiguration.Base;
using CostControl.Entity.Models.CostControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CostControl.Data.EntityConfiguration.CostControl
{
    public class OverCostConfigure : BaseEntityConfigure<OverCost, long>
    {
        public override void Configure(EntityTypeBuilder<OverCost> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(e => e.StartDate)
                .HasColumnType("datetime");

            entityTypeBuilder
                .Property(e => e.EndDate)
                .HasColumnType("datetime");

            entityTypeBuilder
                .Property(e => e.RegisteredDate)
                .HasColumnType("datetime");

            entityTypeBuilder
               .ToTable("OverCost", "dbo");
        }
    }
}