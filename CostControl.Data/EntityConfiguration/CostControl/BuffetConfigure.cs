using CostControl.Data.EntityConfiguration.Base;
using CostControl.Entity.Models.CostControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CostControl.Data.EntityConfiguration.CostControl
{
    public class BuffetConfigure : BaseEntityConfigure<Buffet, long>
    {
        public override void Configure(EntityTypeBuilder<Buffet> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);
            
            entityTypeBuilder
               .ToTable("Buffet", "dbo");
        }
    }
}