namespace CostControl.Data.EntityConfiguration.CostControl
{
    using Data.EntityConfiguration.Base;
    using Entity.Models.CostControl;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class DepoConfigure : BaseEntityConfigure<Depo, long>
    {
        public override void Configure(EntityTypeBuilder<Depo> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(e => e.SaleCostPointId)
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
               .ToTable("Depo", "dbo");
        }
    }
}