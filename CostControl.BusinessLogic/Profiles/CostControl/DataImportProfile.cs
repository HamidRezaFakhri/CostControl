namespace CostControl.BusinessLogic.Profiles.CostControl
{
    using AutoMapper;

    public class DataImportProfile : Profile
    {
        public override string ProfileName => GetType().Name;

        public DataImportProfile()
        {
            CreateMap<Entity.Models.CostControl.DataImport, BusinessEntity.Models.CostControl.DataImport>(MemberList.None)
                .ReverseMap();
        }
    }
}
