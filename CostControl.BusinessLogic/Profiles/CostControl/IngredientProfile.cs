namespace CostControl.BusinessLogic.Profiles.CostControl
{
    using AutoMapper;

    public class IngredientProfile : Profile
    {
        public override string ProfileName => GetType().Name;

        public IngredientProfile()
        {
            CreateMap<Entity.Models.CostControl.Ingredient, BusinessEntity.Models.CostControl.Ingredient>(MemberList.None)
                .ReverseMap();
        }
    }
}
