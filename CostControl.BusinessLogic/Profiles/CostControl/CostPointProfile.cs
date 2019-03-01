namespace CostControl.BusinessLogic.Profiles.CostControl
{
    using AutoMapper;

    public class CostPointProfile : Profile
    {
        public override string ProfileName => GetType().Name;

        public CostPointProfile()
        {
            CreateMap<Entity.Models.CostControl.CostPoint, BusinessEntity.Models.CostControl.CostPoint>(MemberList.None)
                //.ForMember(dest => dest.CostPointGroupId, opt => opt.MapFrom(src => src.CostPointGroup.Id))
                //.ForMember(dest => dest.CostPointGroupName, opt => opt.MapFrom(src => src.CostPointGroup.Name))
                //.ForMember(dest => dest.CostPointGroup, opt => opt.Ignore())
                //.ForMember(dest => dest.CostPointGroup, opt => opt.MapFrom(src => src.CostPointGroup))
                //.PreserveReferences()
                .ReverseMap();
        }
    }
}
