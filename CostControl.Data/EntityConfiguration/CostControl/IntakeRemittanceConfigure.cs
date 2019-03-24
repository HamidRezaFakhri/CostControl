namespace CostControl.Data.EntityConfiguration.CostControl
{
	using Data.EntityConfiguration.Base;
	using Entity.Models.CostControl;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class IntakeRemittanceConfigure : BaseEntityConfigure<IntakeRemittance, long>
	{
		public override void Configure(EntityTypeBuilder<IntakeRemittance> entityTypeBuilder)
		{
			base.Configure(entityTypeBuilder);
			
			entityTypeBuilder
				.Property(e => e.IntakeDate)
				.HasColumnType("datetime");

			entityTypeBuilder
				.Property(e => e.RegisteredDate)
				.HasColumnType("datetime");

			entityTypeBuilder
				.ToTable("IntakeRemittance", "dbo");
		}
	}
}