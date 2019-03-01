namespace CostControl.BusinessLogic.Profiles.CostControl
{
    using AutoMapper;

    public class OverCostProfile : Profile
    {
        public override string ProfileName => GetType().Name;

        public OverCostProfile()
        {
            CreateMap<Entity.Models.CostControl.OverCost, BusinessEntity.Models.CostControl.OverCost>(MemberList.None)
                .ReverseMap();
        }
    }
}
