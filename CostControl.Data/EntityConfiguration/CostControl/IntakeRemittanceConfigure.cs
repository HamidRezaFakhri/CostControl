namespace CostControl.Data.EntityConfiguration.CostControl
{
    using System;
    using Data.EntityConfiguration.Base;
    using Entity.Models.CostControl;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class IntakeRemittanceConfigure : BaseEntityConfigure<IntakeRemittance, long>
    {
        public override void Configure(EntityTypeBuilder<IntakeRemittance> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(e => e.SaleCostPointId)
                .IsRequired();

            entityTypeBuilder
                .Property(e => e.IntakeDate)
                .IsRequired();

            entityTypeBuilder
                .HasIndex(e => new { e.SaleCostPointId, e.IntakeDate })
                .IsUnique();

            entityTypeBuilder
                .Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode()
                .IsRequired();

            entityTypeBuilder
                .Property(e => e.RegisteredDate)
                .HasDefaultValue(DateTime.Now)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            entityTypeBuilder
                .Property(e => e.RegisteredUserId)
                .IsRequired();

            entityTypeBuilder
                .ToTable("IntakeRemittance", "dbo");
        }
    }
}