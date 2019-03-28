namespace CostControl.Data.EntityConfiguration.CostControl
{
    using Data.EntityConfiguration.Base;
    using Entity.Models.CostControl;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BuffetConfigure : BaseEntityConfigure<Buffet, long>
    {
        public override void Configure(EntityTypeBuilder<Buffet> entityTypeBuilder)
        {
            base.Configure(entityTypeBuilder);

            entityTypeBuilder
                .Property(e => e.SaleCostPointId)
                .IsRequired();

            entityTypeBuilder
               .ToTable("Buffet", "dbo");
        }
    }
}