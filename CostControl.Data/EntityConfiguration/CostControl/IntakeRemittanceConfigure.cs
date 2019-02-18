using CostControl.Data.EntityConfiguration.Base;
using CostControl.Entity.Models.CostControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CostControl.Data.EntityConfiguration.CostControl
{
    public class IntakeRemittanceConfigure : BaseEntityConfigure<IntakeRemittance, long>
    {
        public override void Configure(EntityTypeBuilder<IntakeRemittance> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);


            entityTypeBuilder
                .Property(e => e.IntakeDate)
                .HasColumnType("datetime");

            entityTypeBuilder
                .Property(e => e.RegisteredDate)
                .HasColumnType("datetime");

            entityTypeBuilder
                .ToTable("IntakeRemittance", "dbo");
        }
    }
}