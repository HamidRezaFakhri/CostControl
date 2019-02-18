using CostControl.Data.EntityConfiguration.Base;
using CostControl.Entity.Models.CostControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CostControl.Data.EntityConfiguration.CostControl
{
    public class IncommingUserConfigure : BaseEntityConfigure<IncommingUser, int>
    {
        public override void Configure(EntityTypeBuilder<IncommingUser> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);
            
            entityTypeBuilder
               .ToTable("IncommingUser", "dbo");
        }
    }
}