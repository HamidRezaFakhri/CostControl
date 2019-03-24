namespace CostControl.Data.EntityConfiguration.CostControl
{
	using Data.EntityConfiguration.Base;
	using Entity.Models.CostControl;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class MenuConfigure : BaseEntityConfigure<Menu, long>
	{
		public override void Configure(EntityTypeBuilder<Menu> entityTypeBuilder)
		{
			base.Configure(entityTypeBuilder);

			entityTypeBuilder
				.Property(e => e.FromDate)
				.HasColumnType("datetime");

			entityTypeBuilder
				.Property(e => e.ToDate)
				.HasColumnType("datetime");
			
			entityTypeBuilder
				.HasIndex(e => new { e.FromDate, e.ToDate })
				.IsUnique();

			entityTypeBuilder
			   .ToTable("Menu", "dbo");
		}
	}
}