namespace CostControl.BusinessLogic.Profiles.CostControl
{
    using AutoMapper;

    public class SaleCostPointProfile : Profile
    {
        public override string ProfileName => GetType().Name;

        public SaleCostPointProfile()
        {
            CreateMap<Entity.Models.CostControl.SaleCostPoint, BusinessEntity.Models.CostControl.SaleCostPoint>(MemberList.None)
                //.ForMember(dest => dest.CostPointName, opt => opt.MapFrom(src => src.CostPoint.Name))
                //.ForMember(dest => dest.SalePointName, opt => opt.MapFrom(src => src.SalePoint.Name))
                .ReverseMap();
        }
    }
}
