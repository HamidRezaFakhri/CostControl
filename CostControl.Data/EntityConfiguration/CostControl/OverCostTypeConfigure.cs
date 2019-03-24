﻿namespace CostControl.Data.EntityConfiguration.CostControl
{
	using Data.EntityConfiguration.Base;
	using Entity.Models.CostControl;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class OverCostTypeConfigure : BaseNamedEntityConfigure<OverCostType, byte>
	{
		public override void Configure(EntityTypeBuilder<OverCostType> entityTypeBuilder)
		{
			base.Configure(entityTypeBuilder);

			entityTypeBuilder
				.HasIndex(e => e.Name)
				.IsUnique();

			entityTypeBuilder
			   .ToTable("OverCostType", "dbo");
		}
	}
}