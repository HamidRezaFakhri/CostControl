namespace CostControl.Data.EntityConfiguration.Base
{
	using Entity.Models.Base;
	using Entity.Models.Base.Enums;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public abstract class BaseEntityConfigure<TEntity, TKey> :
			IEntityTypeConfiguration<TEntity> where TEntity : SuperEntity<TKey>
	{
		public virtual void Configure(EntityTypeBuilder<TEntity> entityTypeBuilder)
		{
			entityTypeBuilder
				.HasKey(e => e.Id);

			entityTypeBuilder
				.Property(e => e.Id)
				.IsRequired()
				.ValueGeneratedOnAdd();

			//entityTypeBuilder
			//    .HasKey(e => e.Id);

			//entityTypeBuilder
			//    .Property(e => e.InstanceId)
			//    .IsRequired()
			//    .HasColumnType("uniqueidentifier")
			//    .IsConcurrencyToken()
			//    .HasDefaultValue(System.Guid.NewGuid());

			entityTypeBuilder
				.Property(e => e.State)
				.IsRequired()
				.HasDefaultValue(ObjectState.Active)
				.HasDefaultValueSql("1");
		}
	}
}