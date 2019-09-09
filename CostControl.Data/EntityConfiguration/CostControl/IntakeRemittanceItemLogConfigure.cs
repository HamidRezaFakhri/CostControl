namespace CostControl.Data.EntityConfiguration.CostControl
{
    using Data.EntityConfiguration.Base;
    using Entity.Models.CostControl;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class IntakeRemittanceItemLogConfigure : BaseEntityConfigure<IntakeRemittanceItemLog, long>
    {
        public override void Configure(EntityTypeBuilder<IntakeRemittanceItemLog> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(e => e.IntakeRemittanceItemId)
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
                .Property(e => e.Descripton)
                .HasMaxLength(1000)
                .HasColumnType("NVARCHAR(1000)")
                .IsRequired()
                .IsUnicode();

            entityTypeBuilder
               .ToTable("IntakeRemittanceItemLog", "dbo");
        }
    }
}