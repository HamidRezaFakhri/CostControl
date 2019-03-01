namespace CostControl.BusinessLogic.Profiles.CostControl
{
    using AutoMapper;

    public class DepoProfile : Profile
    {
        public override string ProfileName => GetType().Name;

        public DepoProfile()
        {
            CreateMap<Entity.Models.CostControl.Depo, BusinessEntity.Models.CostControl.Depo>(MemberList.None)
                .ReverseMap();
        }
    }
}
