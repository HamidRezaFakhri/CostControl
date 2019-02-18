using CostControl.Data.EntityConfiguration.Base;
using CostControl.Entity.Models.CostControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CostControl.Data.EntityConfiguration.CostControl
{
    public class IntakeRemittanceItemConfigure : BaseEntityConfigure<IntakeRemittanceItem, long>
    {
        public override void Configure(EntityTypeBuilder<IntakeRemittanceItem> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);
            
            entityTypeBuilder
               .ToTable("IntakeRemittanceItem", "dbo");
        }
    }
}