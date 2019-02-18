using CostControl.Data.EntityConfiguration.Base;
using CostControl.Entity.Models.CostControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CostControl.Data.EntityConfiguration.CostControl
{
    public class DraftItemConfigure : BaseEntityConfigure<DraftItem, long>
    {
        public override void Configure(EntityTypeBuilder<DraftItem> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);
            
            entityTypeBuilder
               .ToTable("DraftItem", "dbo");
        }
    }
}