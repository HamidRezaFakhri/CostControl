namespace CostControl.Data.EntityConfiguration.CostControl
{
	using Data.EntityConfiguration.Base;
	using Entity.Models.CostControl;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class MenuItemConfigure : BaseEntityConfigure<MenuItem, long>
	{
		public override void Configure(EntityTypeBuilder<MenuItem> entityTypeBuilder)
		{
			base.Configure(entityTypeBuilder);

			entityTypeBuilder
			   .ToTable("MenuItem", "dbo");
		}
	}
}