namespace CostControl.Data.EntityConfiguration.CostControl
{
    using Data.EntityConfiguration.Base;
    using Entity.Models.CostControl;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class DraftItemConfigure : BaseEntityConfigure<DraftItem, long>
    {
        public override void Configure(EntityTypeBuilder<DraftItem> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(e => e.DraftId)
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
                .HasIndex(e => new { e.DraftId, e.IngredientId })
                .IsUnique();

            entityTypeBuilder
               .ToTable("DraftItem", "dbo");
        }
    }
}