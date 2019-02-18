using CostControl.Data.EntityConfiguration.Base;
using CostControl.Entity.Models.CostControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CostControl.Data.EntityConfiguration.CostControl
{
    public class DraftConfigure : BaseEntityConfigure<Draft, long>
    {
        public override void Configure(EntityTypeBuilder<Draft> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(e => e.DraftDate)
                .HasColumnType("datetime");

            entityTypeBuilder
                .Property(e => e.RegisteredDate)
                .HasColumnType("datetime");

            entityTypeBuilder
               .ToTable("Draft", "dbo");
        }
    }
}