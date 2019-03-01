namespace CostControl.BusinessLogic.Mapper
{
    using AutoMapper;
    using AutoMapper.EquivalencyExpression;
    using AutoMapper.Extensions.ExpressionMapping;
    using CostControl.BusinessLogic.Profiles.CostControl;
    using CostControl.BusinessLogic.Profiles.Security;

    public class AutoMapperConfiguration
    {
        public MapperConfiguration Configure()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddCollectionMappers();
                cfg.AllowNullCollections = true;
                cfg.AddExpressionMapping();
                cfg.ValidateInlineMaps = false;
                //cfg.ValueTransformers

                //cfg.ConstructServicesUsing();

                //cfg.AddProfile<ClientMappingProfile>();
                
                cfg.AddProfile<BuffetProfile>();
                cfg.AddProfile<ConsumptionUnitProfile>();
                cfg.AddProfile<CostPointGroupProfile>();
                cfg.AddProfile<CostPointProfile>();
                cfg.AddProfile<DataImportProfile>();
                cfg.AddProfile<DepoProfile>();
                cfg.AddProfile<DraftProfile>();
                cfg.AddProfile<DraftItemProfile>();
                cfg.AddProfile<FoodProfile>();
                cfg.AddProfile<IncommingUserProfile>();
                cfg.AddProfile<IngredientProfile>();
                cfg.AddProfile<IntakeRemittanceProfile>();
                cfg.AddProfile<IntakeRemittanceItemProfile>();
                cfg.AddProfile<InventoryProfile>();
                cfg.AddProfile<MenuProfile>();
                cfg.AddProfile<MenuItemProfile>();
                cfg.AddProfile<OverCostTypeProfile>();
                cfg.AddProfile<OverCostProfile>();
                cfg.AddProfile<RecipeProfile>();
                cfg.AddProfile<SaleCostPointProfile>();
                cfg.AddProfile<SalePointProfile>();
                cfg.AddProfile<SettingProfile>();

                //cfg.AddProfile<RoleProfile>();

                //cfg.AddProfiles(typeof(ClientMappingProfile).Assembly);

            });

            // Test Mapping and Configuration Validation
            config.AssertConfigurationIsValid();

            return config;
        }
    }
}