namespace CostControl.BusinessLogic.Profiles.CostControl
{
    using AutoMapper;

    public class SalePointProfile : Profile
    {
        public override string ProfileName => GetType().Name;

        public SalePointProfile()
        {
            CreateMap<Entity.Models.CostControl.SalePoint, BusinessEntity.Models.CostControl.SalePoint>(MemberList.None)
                //.ForMember(dest => dest.Users, opt => opt.Ignore())
                //.ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users))
                .ReverseMap();
        }
    }
}
