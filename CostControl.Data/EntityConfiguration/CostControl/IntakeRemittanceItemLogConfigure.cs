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
                .Property(e => e.Description)
                .HasMaxLength(1000)
                .HasColumnType("NVARCHAR(1000)")
                .IsRequired()
                .IsUnicode();


            entityTypeBuilder
                .Property(e => e.LogDate)
                .HasDefaultValue(System.DateTime.Now)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            entityTypeBuilder
                .Property(e => e.LogUserId)
                .IsRequired();

            entityTypeBuilder
                .HasOne(e => e.LogUser)
                .WithMany(iu => iu.IntakeRemittanceItemLogs)
                .HasForeignKey(oc => oc.LogUserId)
                .OnDelete(DeleteBehavior.Restrict);

            entityTypeBuilder
               .ToTable("IntakeRemittanceItemLog", "dbo");
        }
    }
}