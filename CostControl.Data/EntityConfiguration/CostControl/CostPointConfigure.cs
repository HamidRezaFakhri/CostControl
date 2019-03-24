namespace CostControl.Data.EntityConfiguration.CostControl
{
	using Data.EntityConfiguration.Base;
	using Entity.Models.CostControl;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class CostPointConfigure : BaseNamedEntityConfigure<CostPoint, long>
	{
		public override void Configure(EntityTypeBuilder<CostPoint> entityTypeBuilder)
		{
			base.Configure(entityTypeBuilder);

			entityTypeBuilder
				.HasIndex(e => e.Name)
				.IsUnique();

			entityTypeBuilder
			   .ToTable("CostPoint", "dbo");
		}
	}
}