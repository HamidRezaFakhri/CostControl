using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;

namespace CostControl.BusinessLogic.Mapper
{
    public class AutoMapperConfiguration
    {
        public MapperConfiguration Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                //cfg.AddCollectionMappers();
                cfg.AllowNullCollections = true;
                cfg.AddExpressionMapping();
                cfg.ValidateInlineMaps = false;
                //cfg.ValueTransformers
                cfg.AddProfile<ClientMappingProfile>();
            });

            // Test Mapping and Configuration Validation
            config.AssertConfigurationIsValid();
            
            return config;
        }
    }
}