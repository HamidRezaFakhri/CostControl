namespace CostControl.Data.EntityConfiguration.CostControl
{
	using Data.EntityConfiguration.Base;
	using Entity.Models.CostControl;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class CostPointGroupConfigure : BaseNamedEntityConfigure<CostPointGroup, long>
	{
		public override void Configure(EntityTypeBuilder<CostPointGroup> entityTypeBuilder)
		{
			base.Configure(entityTypeBuilder);

			entityTypeBuilder
				.HasIndex(e => e.Name)
				.IsUnique();

			entityTypeBuilder
			   .ToTable("CostPointGroup", "dbo");
		}
	}
}