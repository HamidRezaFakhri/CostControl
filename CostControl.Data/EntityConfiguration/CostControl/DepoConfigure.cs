namespace CostControl.Data.EntityConfiguration.CostControl
{
	using Data.EntityConfiguration.Base;
	using Entity.Models.CostControl;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class DepoConfigure : BaseEntityConfigure<Depo, long>
	{
		public override void Configure(EntityTypeBuilder<Depo> entityTypeBuilder)
		{
			base.Configure(entityTypeBuilder);

			entityTypeBuilder
			   .ToTable("Depo", "dbo");
		}
	}
}