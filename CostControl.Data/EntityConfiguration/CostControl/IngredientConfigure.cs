using CostControl.Data.EntityConfiguration.Base;
using CostControl.Entity.Models.CostControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CostControl.Data.EntityConfiguration.CostControl
{
    public class IngredientConfigure : BaseNamedEntityConfigure<Ingredient, long>
    {
        public override void Configure(EntityTypeBuilder<Ingredient> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .HasIndex(e => new { e.Name, e.Code })
                .IsUnique();

            entityTypeBuilder
                .Property(e => e.UsefullRatio)
                .IsRequired()
                .HasDefaultValue(70)
                .HasDefaultValueSql("70");

            entityTypeBuilder
                .Property(e => e.Description)
                .HasMaxLength(250)
                .HasColumnType("NVarChar(250)");

            entityTypeBuilder
               .ToTable("Ingredient", "dbo");
        }
    }
}