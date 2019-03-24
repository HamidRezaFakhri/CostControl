namespace CostControl.Data.EntityConfiguration.CostControl
{
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
				.Property(e => e.StartDate)
				.HasColumnType("datetime");

			entityTypeBuilder
				.Property(e => e.EndDate)
				.HasColumnType("datetime");
			
			entityTypeBuilder
				.HasIndex(e => new { e.StartDate, e.EndDate })
				.IsUnique();

			entityTypeBuilder
				.Property(e => e.RegisteredDate)
				.HasColumnType("datetime");

			entityTypeBuilder
			   .ToTable("OverCost", "dbo");
		}
	}
}