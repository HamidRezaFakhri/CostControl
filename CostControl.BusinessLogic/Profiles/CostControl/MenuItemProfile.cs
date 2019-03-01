namespace CostControl.BusinessLogic.Profiles.CostControl
{
    using AutoMapper;

    public class MenuItemProfile : Profile
    {
        public override string ProfileName => GetType().Name;

        public MenuItemProfile()
        {
            CreateMap<Entity.Models.CostControl.MenuItem, BusinessEntity.Models.CostControl.MenuItem>(MemberList.None)
                .ReverseMap();
        }
    }
}
