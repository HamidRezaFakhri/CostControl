namespace CostControl.Data.EntityConfiguration.CostControl
{
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
				.HasColumnType("DateTime");

			entityTypeBuilder
			   .ToTable("DataImport", "dbo");
		}
	}
}