namespace CostControl.Data.EntityConfiguration.CostControl
{
	using Data.EntityConfiguration.Base;
	using Entity.Models.CostControl;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class FoodConfigure : BaseNamedEntityConfigure<Food, long>
	{
		public override void Configure(EntityTypeBuilder<Food> entityTypeBuilder)
		{
			base.Configure(entityTypeBuilder);

			entityTypeBuilder
				.HasIndex(e => e.Name)
				.IsUnique();

			entityTypeBuilder
			   .ToTable("Food", "dbo");
		}
	}
}