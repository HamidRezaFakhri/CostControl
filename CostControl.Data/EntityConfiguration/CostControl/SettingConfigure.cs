namespace CostControl.Data.EntityConfiguration.CostControl
{
	using Data.EntityConfiguration.Base;
	using Entity.Models.CostControl;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class SettingConfigure : BaseEntityConfigure<Setting, int>
	{
		public override void Configure(EntityTypeBuilder<Setting> entityTypeBuilder)
		{
			base.Configure(entityTypeBuilder);

			entityTypeBuilder
			   .ToTable("Setting", "dbo");
		}
	}
}