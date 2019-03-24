namespace CostControl.Data.EntityConfiguration.Security
{
	//public class RoleConfigure : BaseNamedEntityConfigure<Role, long>
	//{
	//    public override void Configure(EntityTypeBuilder<Role> entityTypeBuilder)
	//    {
	//        base.Configure(entityTypeBuilder);

	//        //// Each Role can have many entries in the UserRole join table
	//        //entityTypeBuilder
	//        //    .HasMany<UserRole>()
	//        //    .WithOne()
	//        //    .HasForeignKey(ur => ur.RoleId)
	//        //    .IsRequired();

	//        //// Each Role can have many associated RoleClaims
	//        //entityTypeBuilder
	//        //    .HasMany<RoleClaim>()
	//        //    .WithOne()
	//        //    .HasForeignKey(rc => rc.RoleId)
	//        //    .IsRequired();

	//        entityTypeBuilder
	//           .ToTable("Role", "sec");

	//        //entityTypeBuilder
	//        //    .HasData(
	//        //        new Role
	//        //            {
	//        //                Code = "01",
	//        //                Name = "Admin",
	//        //                State = Entity.Models.Base.Enums.ObjectState.Active
	//        //        });
	//    }
	//}
}