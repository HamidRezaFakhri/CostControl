namespace CostControl.Data.EntityConfiguration.CostControl
{
    using System;
    using Data.EntityConfiguration.Base;
    using Entity.Models.CostControl;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MenuConfigure : BaseEntityConfigure<Menu, long>
    {
        public override void Configure(EntityTypeBuilder<Menu> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(e => e.SaleCostPointId)
                .IsRequired();

            entityTypeBuilder
                .Property(e => e.FromDate)
                .HasDefaultValue(DateTime.Now)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            entityTypeBuilder
                .Property(e => e.Code)
                .HasMaxLength(10);

            entityTypeBuilder
                .HasIndex(e => e.Code)
                .IsUnique();

            entityTypeBuilder
                .Property(e => e.EnglishName)
                .HasMaxLength(250)
                .IsRequired();

            entityTypeBuilder
                .HasIndex(e => e.EnglishName)
                .IsUnique();

            entityTypeBuilder
                .HasIndex(e => new { e.FromDate, e.ToDate })
                .IsUnique();

            entityTypeBuilder
                .Property(e => e.CostRatio)
                .HasDefaultValue(100)
                .HasDefaultValueSql("100")
                .IsRequired();

            entityTypeBuilder
                .Property(e => e.CostRatio)
                .HasColumnType("numeric(28,2)")
                .HasDefaultValue(0)
                .HasDefaultValueSql("0")
                .IsRequired();

            //entityTypeBuilder
            //    .Property(e => e.MenuItems)
            //    .IsRequired();

            entityTypeBuilder
               .ToTable("Menu", "dbo");
        }
    }
}