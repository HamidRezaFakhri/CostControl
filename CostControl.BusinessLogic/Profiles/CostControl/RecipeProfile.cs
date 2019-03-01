namespace CostControl.BusinessLogic.Profiles.CostControl
{
    using AutoMapper;

    public class RecipeProfile : Profile
    {
        public override string ProfileName => GetType().Name;

        public RecipeProfile()
        {
            CreateMap<Entity.Models.CostControl.Recipe, BusinessEntity.Models.CostControl.Recipe>(MemberList.None)
                .ReverseMap();
        }
    }
}
