namespace CostControl.BusinessLogic.Profiles.CostControl
{
    using AutoMapper;

    public class InventoryProfile : Profile
    {
        public override string ProfileName => GetType().Name;

        public InventoryProfile()
        {
            CreateMap<Entity.Models.CostControl.Inventory, BusinessEntity.Models.CostControl.Inventory>(MemberList.None)
                .ReverseMap();
        }
    }
}
