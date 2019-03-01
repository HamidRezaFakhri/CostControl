namespace CostControl.BusinessLogic.Profiles.CostControl
{
    using AutoMapper;

    public class DraftItemProfile : Profile
    {
        public override string ProfileName => GetType().Name;

        public DraftItemProfile()
        {
            CreateMap<Entity.Models.CostControl.DraftItem, BusinessEntity.Models.CostControl.DraftItem>(MemberList.None)
                .ReverseMap();
        }
    }
}
