namespace CostControl.Data.EntityConfiguration.CostControl
{
    using Data.EntityConfiguration.Base;
    using Entity.Models.CostControl;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class IngredientConfigure : BaseEntityConfigure<Ingredient, long>
    {
        public override void Configure(EntityTypeBuilder<Ingredient> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(e => e.Name)
                .HasMaxLength(250)
                .HasColumnType("NVARCHAR(250)")
                .IsRequired()
                .IsUnicode();

            entityTypeBuilder
                .HasIndex(e => new { e.Name, e.Code })
                .IsUnique();

            entityTypeBuilder
                .Property(e => e.Code)
                .HasMaxLength(10);

            entityTypeBuilder
                .Property(e => e.EnglishName)
                .HasMaxLength(250);
            //.IsRequired();
            
            entityTypeBuilder
                .Property(e => e.ConsumptionUnitId)
                .IsRequired();

            //entityTypeBuilder
            //    .Property(e => e.Price)
            //    .HasColumnType("numeric(28,2)")
            //    .IsRequired();

            entityTypeBuilder
                    .Property(e => e.UsefullRatio)
                    .IsRequired()
                    .HasDefaultValue(70)
                    .HasDefaultValueSql("70");

            entityTypeBuilder
                .Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnType("NVARCHAR(500)")
                .IsUnicode();

            entityTypeBuilder
               .ToTable("Ingredient", "dbo");
        }
    }
}