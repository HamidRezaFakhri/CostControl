namespace CostControl.Data.EntityConfiguration.CostControl
{
	using Data.EntityConfiguration.Base;
	using Entity.Models.CostControl;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class IntakeRemittanceItemConfigure : BaseEntityConfigure<IntakeRemittanceItem, long>
	{
		public override void Configure(EntityTypeBuilder<IntakeRemittanceItem> entityTypeBuilder)
		{
			base.Configure(entityTypeBuilder);

			entityTypeBuilder
			   .ToTable("IntakeRemittanceItem", "dbo");
		}
	}
}