namespace CostControl.BusinessLogic.Profiles.CostControl
{
    using AutoMapper;

    public class IntakeRemittanceItemProfile : Profile
    {
        public override string ProfileName => GetType().Name;

        public IntakeRemittanceItemProfile()
        {
            CreateMap<Entity.Models.CostControl.IntakeRemittanceItem, BusinessEntity.Models.CostControl.IntakeRemittanceItem>(MemberList.None)
                .ReverseMap();
        }
    }
}
