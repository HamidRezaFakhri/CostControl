namespace CostControl.BusinessLogic.Profiles.CostControl
{
    using AutoMapper;

    public class IncommingUserProfile : Profile
    {
        public override string ProfileName => GetType().Name;

        public IncommingUserProfile()
        {
            CreateMap<Entity.Models.CostControl.IncommingUser, BusinessEntity.Models.CostControl.IncommingUser>(MemberList.None)
                .ReverseMap();
        }
    }
}
