using AutoMapper;

namespace CostControl.BusinessLogic.Mapper
{
    public class AutoMapperConfiguration
    {
        public MapperConfiguration Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ClientMappingProfile>();
            });
            
            // Test Mapping and Configuration Validation
            config.AssertConfigurationIsValid();

            return config;
        }
    }
}