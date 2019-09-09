namespace CostControl.BusinessLogic.Profiles.CostControl
{
    using AutoMapper;

    public class IntakeRemittanceItemLogProfile : Profile
    {
        public override string ProfileName => GetType().Name;

        public IntakeRemittanceItemLogProfile()
        {
            CreateMap<Entity.Models.CostControl.IntakeRemittanceItemLog, BusinessEntity.Models.CostControl.IntakeRemittanceItemLog>(MemberList.None)
                .ReverseMap();
        }
    }
}
