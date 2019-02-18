using CostControl.Data.EntityConfiguration.Base;
using CostControl.Entity.Models.CostControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CostControl.Data.EntityConfiguration.CostControl
{
    public class SaleConfigure : BaseEntityConfigure<Sale, long>
    {
        public override void Configure(EntityTypeBuilder<Sale> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(e => e.SaleDate)
                .HasColumnType("datetime");

            entityTypeBuilder
               .ToTable("Sale", "dbo");
        }
    }
}