using CostControl.Data.EntityConfiguration.Base;
using CostControl.Entity.Models.CostControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CostControl.Data.EntityConfiguration.CostControl
{
    public class SaleItemConfigure : BaseEntityConfigure<SaleItem, long>
    {
        public override void Configure(EntityTypeBuilder<SaleItem> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);
            
            entityTypeBuilder
               .ToTable("SaleItem", "dbo");
        }
    }
}