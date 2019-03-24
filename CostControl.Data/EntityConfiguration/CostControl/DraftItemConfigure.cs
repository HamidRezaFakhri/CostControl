namespace CostControl.Data.EntityConfiguration.CostControl
{
	using Data.EntityConfiguration.Base;
	using Entity.Models.CostControl;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class DraftItemConfigure : BaseEntityConfigure<DraftItem, long>
	{
		public override void Configure(EntityTypeBuilder<DraftItem> entityTypeBuilder)
		{
			base.Configure(entityTypeBuilder);

			entityTypeBuilder
				.HasIndex(e => new { e.DraftId, e.IngredientId })
				.IsUnique();

			entityTypeBuilder
			   .ToTable("DraftItem", "dbo");
		}
	}
}