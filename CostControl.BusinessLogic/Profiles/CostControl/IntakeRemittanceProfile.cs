namespace CostControl.BusinessLogic.Profiles.CostControl
{
    using AutoMapper;

    public class IntakeRemittanceProfile : Profile
    {
        public override string ProfileName => GetType().Name;

        public IntakeRemittanceProfile()
        {
            CreateMap<Entity.Models.CostControl.IntakeRemittance, BusinessEntity.Models.CostControl.IntakeRemittance>(MemberList.None)
                .ReverseMap();
        }
    }
}
