namespace CostControl.Data.EntityConfiguration.CostControl
{
	using Data.EntityConfiguration.Base;
	using Entity.Models.CostControl;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class SaleCostPointConfigure : BaseEntityConfigure<SaleCostPoint, long>
	{
		public override void Configure(EntityTypeBuilder<SaleCostPoint> entityTypeBuilder)
		{
			base.Configure(entityTypeBuilder);

			entityTypeBuilder
				.HasIndex(e => new { e.SalePointId, e.CostPointId })
				.IsUnique();

			entityTypeBuilder
				.HasOne(sc => sc.SalePoint)
				.WithMany(s => s.SaleCostPoints)
				.HasForeignKey(sc => sc.SalePointId)
				.OnDelete(DeleteBehavior.Restrict);

			entityTypeBuilder
				.HasOne(sc => sc.CostPoint)
				.WithMany(c => c.SaleCostPoints)
				.HasForeignKey(sc => sc.CostPointId)
				.OnDelete(DeleteBehavior.Restrict);

			entityTypeBuilder
			   .ToTable("SaleCostPoint", "dbo");
		}
	}
}