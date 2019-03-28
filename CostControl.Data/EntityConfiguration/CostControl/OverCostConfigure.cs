namespace CostControl.Data.EntityConfiguration.CostControl
{
    using System;
    using Data.EntityConfiguration.Base;
    using Entity.Models.CostControl;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class OverCostConfigure : BaseEntityConfigure<OverCost, long>
    {
        public override void Configure(EntityTypeBuilder<OverCost> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(e => e.SaleCostPointId)
                .IsRequired();

            entityTypeBuilder
                .Property(e => e.OverCostTypeId)
                .IsRequired();

            entityTypeBuilder
                .Property(e => e.StartDate)
                .IsRequired();

            entityTypeBuilder
                .Property(e => e.EndDate)
                .IsRequired();

            entityTypeBuilder
                .Property(e => e.Price)
                .IsRequired();

            entityTypeBuilder
                .HasIndex(e => new { e.StartDate, e.EndDate })
                .IsUnique();

            entityTypeBuilder
                .Property(e => e.RegisteredDate)
                .HasDefaultValue(DateTime.Now)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            entityTypeBuilder
                .Property(e => e.RegisteredUserId)
                .IsRequired();

            entityTypeBuilder
               .ToTable("OverCost", "dbo");
        }
    }
}