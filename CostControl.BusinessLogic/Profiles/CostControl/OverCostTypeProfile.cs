namespace CostControl.BusinessLogic.Profiles.CostControl
{
    using AutoMapper;

    public class OverCostTypeProfile : Profile
    {
        public override string ProfileName => GetType().Name;

        public OverCostTypeProfile()
        {
            CreateMap<Entity.Models.CostControl.OverCostType, BusinessEntity.Models.CostControl.OverCostType>(MemberList.None)
                .ReverseMap();
        }
    }
}
