namespace CostControl.Data.EntityConfiguration.CostControl
{
    using System;
    using Data.EntityConfiguration.Base;
    using Entity.Models.CostControl;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class DraftConfigure : BaseEntityConfigure<Draft, long>
    {
        public override void Configure(EntityTypeBuilder<Draft> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(e => e.SaleCostPointId)
                .IsRequired();

            entityTypeBuilder
                .Property(e => e.InventoryId)
                .IsRequired();

            entityTypeBuilder
                .Property(e => e.DraftDate)
                .HasDefaultValue(DateTime.Now)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            entityTypeBuilder
                .Property(e => e.RegisteredDate)
                .HasDefaultValue(DateTime.Now)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            entityTypeBuilder
                .Property(e => e.RegisteredUserId)
                .IsRequired();

            //entityTypeBuilder
            //    .Property(e => e.DraftItems)
            //    .IsRequired();

            entityTypeBuilder
                .Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnType("NVARCHAR(500)")
                .IsUnicode();

            entityTypeBuilder
               .ToTable("Draft", "dbo");
        }
    }
}