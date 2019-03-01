namespace CostControl.BusinessLogic.Profiles.CostControl
{
    using AutoMapper;

    public class ConsumptionUnitProfile : Profile
    {
        public override string ProfileName => GetType().Name;

        public ConsumptionUnitProfile()
        {
            CreateMap<Entity.Models.CostControl.ConsumptionUnit, BusinessEntity.Models.CostControl.ConsumptionUnit>(MemberList.None)
                .ReverseMap();
        }
    }
}
