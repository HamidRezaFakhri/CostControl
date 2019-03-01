namespace CostControl.BusinessLogic.Profiles.CostControl
{
    using AutoMapper;

    public class FoodProfile : Profile
    {
        public override string ProfileName => GetType().Name;

        public FoodProfile()
        {
            CreateMap<Entity.Models.CostControl.Food, BusinessEntity.Models.CostControl.Food>(MemberList.None)
                .ReverseMap();
        }
    }
}
