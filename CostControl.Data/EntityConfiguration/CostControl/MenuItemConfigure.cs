using CostControl.Data.EntityConfiguration.Base;
using CostControl.Entity.Models.CostControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CostControl.Data.EntityConfiguration.CostControl
{
    public class MenuItemConfigure : BaseEntityConfigure<MenuItem, long>
    {
        public override void Configure(EntityTypeBuilder<MenuItem> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);
            
            entityTypeBuilder
               .ToTable("MenuItem", "dbo");
        }
    }
}