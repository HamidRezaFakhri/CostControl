namespace CostControl.BusinessLogic.Profiles.Security
{
	using AutoMapper;

	public class RoleProfile : Profile
	{
		public RoleProfile()
		{
			// Map from Role (entity) to Role, and back
			CreateMap<Entity.Models.Security.Role, BusinessEntity.Models.Security.Role>()
				//.ForMember(dest => dest.Users, opt => opt.Ignore())
				//.ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users))
				.ReverseMap();
		}
	}
}