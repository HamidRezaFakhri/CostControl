namespace CostControl.BusinessLogic.Profiles.CostControl
{
    using AutoMapper;

    public class DraftProfile : Profile
    {
        public override string ProfileName => GetType().Name;

        public DraftProfile()
        {
            CreateMap<Entity.Models.CostControl.Draft, BusinessEntity.Models.CostControl.Draft>(MemberList.None)
                .ReverseMap();
        }
    }
}
