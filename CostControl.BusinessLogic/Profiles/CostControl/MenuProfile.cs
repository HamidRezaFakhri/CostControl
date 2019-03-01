namespace CostControl.BusinessLogic.Profiles.CostControl
{
    using AutoMapper;

    public class MenuProfile : Profile
    {
        public override string ProfileName => GetType().Name;

        public MenuProfile()
        {
            CreateMap<Entity.Models.CostControl.Menu, BusinessEntity.Models.CostControl.Menu>(MemberList.None)
                .ReverseMap();
        }
    }
}
