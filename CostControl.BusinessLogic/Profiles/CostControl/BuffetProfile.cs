namespace CostControl.BusinessLogic.Profiles.CostControl
{
    using AutoMapper;

    public class BuffetProfile : Profile
    {
        public override string ProfileName => GetType().Name;

        public BuffetProfile()
        {
            CreateMap<Entity.Models.CostControl.Buffet, BusinessEntity.Models.CostControl.Buffet>(MemberList.None)
                .ReverseMap();
        }
    }
}
