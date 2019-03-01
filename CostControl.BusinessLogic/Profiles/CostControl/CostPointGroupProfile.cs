namespace CostControl.BusinessLogic.Profiles.CostControl
{
    using AutoMapper;

    public class CostPointGroupProfile : Profile
    {
        public override string ProfileName => GetType().Name;

        public CostPointGroupProfile()
        {
            CreateMap<Entity.Models.CostControl.CostPointGroup, BusinessEntity.Models.CostControl.CostPointGroup>(MemberList.None)
                .ReverseMap();
        }
    }
}
