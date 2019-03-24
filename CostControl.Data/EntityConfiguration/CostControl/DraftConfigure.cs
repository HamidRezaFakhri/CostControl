namespace CostControl.Data.EntityConfiguration.CostControl
{
	using Data.EntityConfiguration.Base;
	using Entity.Models.CostControl;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class DraftConfigure : BaseEntityConfigure<Draft, long>
	{
		public override void Configure(EntityTypeBuilder<Draft> entityTypeBuilder)
		{
			base.Configure(entityTypeBuilder);

			entityTypeBuilder
				.Property(e => e.DraftDate)
				.HasColumnType("datetime");

			entityTypeBuilder
				.Property(e => e.RegisteredDate)
				.HasColumnType("datetime");

			entityTypeBuilder
			   .ToTable("Draft", "dbo");
		}
	}
}