namespace CostControl.Data.EntityConfiguration.CostControl
{
    using System;
    using Data.EntityConfiguration.Base;
    using Entity.Models.CostControl;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class DataImportConfigure : BaseEntityConfigure<DataImport, long>
    {
        public override void Configure(EntityTypeBuilder<DataImport> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(e => e.ImportTime)
                .HasDefaultValue(DateTime.Now)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            entityTypeBuilder
               .ToTable("DataImport", "dbo");
        }
    }
}