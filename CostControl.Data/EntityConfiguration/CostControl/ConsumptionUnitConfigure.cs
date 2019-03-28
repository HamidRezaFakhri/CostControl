namespace CostControl.Data.EntityConfiguration.CostControl
{
    using Data.EntityConfiguration.Base;
    using Entity.Models.CostControl;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ConsumptionUnitConfigure : BaseNamedEntityConfigure<ConsumptionUnit, long>
    {
        public override void Configure(EntityTypeBuilder<ConsumptionUnit> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .HasIndex(e => e.Code)
                .IsUnique();

            entityTypeBuilder
                .Property(e => e.Code)
                .HasMaxLength(10);

            entityTypeBuilder
               .ToTable("ConsumptionUnit", "dbo");
        }
    }
}