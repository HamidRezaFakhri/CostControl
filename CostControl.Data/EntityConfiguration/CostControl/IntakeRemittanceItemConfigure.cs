namespace CostControl.Data.EntityConfiguration.CostControl
{
    using Data.EntityConfiguration.Base;
    using Entity.Models.CostControl;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class IntakeRemittanceItemConfigure : BaseEntityConfigure<IntakeRemittanceItem, long>
    {
        public override void Configure(EntityTypeBuilder<IntakeRemittanceItem> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(e => e.IntakeRemittanceId)
                .IsRequired();

            entityTypeBuilder
                .Property(e => e.IngredientId)
                .IsRequired();

            entityTypeBuilder
                .Property(e => e.ConsumptionUnitId)
                .IsRequired();

            entityTypeBuilder
                .Property(e => e.Amount)
                .HasColumnType("numeric(28,2)")
                .IsRequired();

            entityTypeBuilder
                .HasIndex(e => new { e.IntakeRemittanceId, e.IngredientId })
                .IsUnique();

            entityTypeBuilder
               .ToTable("IntakeRemittanceItem", "dbo");
        }
    }
}