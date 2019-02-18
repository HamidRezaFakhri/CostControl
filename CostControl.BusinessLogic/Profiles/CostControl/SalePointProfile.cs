using AutoMapper;

namespace CostControl.BusinessLogic.Profiles.CostControl
{
    public class SalePointProfile : Profile
    {
        public SalePointProfile()
        {
            // Map from Role (entity) to Role, and back
            CreateMap<Entity.Models.CostControl.SalePoint, BusinessEntity.Models.CostControl.SalePoint>()
                //.ForMember(dest => dest.Users, opt => opt.Ignore())
                //.ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users))
                .ReverseMap();
        }
    }
}
