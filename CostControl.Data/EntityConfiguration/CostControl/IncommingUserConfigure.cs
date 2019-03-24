namespace CostControl.Data.EntityConfiguration.CostControl
{
	using Data.EntityConfiguration.Base;
	using Entity.Models.CostControl;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class IncommingUserConfigure : BaseEntityConfigure<IncommingUser, int>
	{
		public override void Configure(EntityTypeBuilder<IncommingUser> entityTypeBuilder)
		{
			base.Configure(entityTypeBuilder);

			entityTypeBuilder
			   .ToTable("IncommingUser", "dbo");
		}
	}
}