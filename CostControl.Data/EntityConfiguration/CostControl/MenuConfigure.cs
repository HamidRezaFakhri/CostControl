using CostControl.Data.EntityConfiguration.Base;
using CostControl.Entity.Models.CostControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CostControl.Data.EntityConfiguration.CostControl
{
    public class MenuConfigure : BaseEntityConfigure<Menu, long>
    {
        public override void Configure(EntityTypeBuilder<Menu> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);
            
            entityTypeBuilder
                .Property(e => e.FromDate)
                .HasColumnType("datetime");

            entityTypeBuilder
                .Property(e => e.ToDate)
                .HasColumnType("datetime");

            entityTypeBuilder
               .ToTable("Menu", "dbo");
        }
    }
}