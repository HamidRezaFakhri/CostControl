namespace CostControl.BusinessLogic.Profiles.CostControl
{
    using AutoMapper;

    public class SettingProfile : Profile
    {
        public override string ProfileName => GetType().Name;

        public SettingProfile()
        {
            CreateMap<Entity.Models.CostControl.Setting, BusinessEntity.Models.CostControl.Setting>(MemberList.None)
                .ReverseMap();
        }
    }
}
