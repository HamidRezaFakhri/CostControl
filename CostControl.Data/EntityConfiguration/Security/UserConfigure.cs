namespace CostControl.Data.EntityConfiguration.Security
{
	//public class UserConfigure : BaseEntityConfigure<User, long>
	//{
	//    public override void Configure(EntityTypeBuilder<User> entityTypeBuilder)
	//    {
	//        base.Configure(entityTypeBuilder);

	//        entityTypeBuilder
	//            .Property(e => e.UserName)
	//            .IsRequired()
	//            .IsUnicode();

	//        entityTypeBuilder
	//            .HasIndex(e => e.UserName)
	//            .IsUnique();

	//        //// Each User can have many UserClaims
	//        //entityTypeBuilder
	//        //    .HasMany<UserClaim>()
	//        //    .WithOne()
	//        //    .HasForeignKey(uc => uc.UserId)
	//        //    .IsRequired();

	//        //// Each User can have many UserLogins
	//        //entityTypeBuilder
	//        //    .HasMany<UserLogin>()
	//        //    .WithOne()
	//        //    .HasForeignKey(ul => ul.UserId)
	//        //    .IsRequired();

	//        //// Each User can have many UserTokens
	//        //entityTypeBuilder
	//        //    .HasMany<UserToken>()
	//        //    .WithOne()
	//        //    .HasForeignKey(ut => ut.UserId)
	//        //    .IsRequired();

	//        //// Each User can have many entries in the UserRole join table
	//        //entityTypeBuilder
	//        //    .HasMany<UserRole>()
	//        //    .WithOne()
	//        //    .HasForeignKey(ur => ur.UserId)
	//        //    .IsRequired();

	//        entityTypeBuilder
	//           .ToTable("User", "sec");

	//        //entityTypeBuilder
	//        //    .HasData(
	//        //        new User
	//        //        {
	//        //            CreatedDate = DateTime.Now,
	//        //            UserName = "Super",
	//        //            Password = "Super",
	//        //            State = Entity.Models.Base.Enums.ObjectState.Active,
	//        //            Email = "Super@Super.com"
	//        //        }
	//        //    );
	//    }
	//}
}