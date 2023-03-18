using Mapster;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.DAL.Entities;
using System.Reflection;

namespace SchoolLibrary_EF.API.Mapping.Configurations
{
    public static class MapsterConfiguration
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            TypeAdapterConfig<PublisherDTO, Publisher>
                .NewConfig()
                .Map(dest => dest.Location, src => $"{src.Street}, {src.City}, {src.Country}")
                // <Publisher, PublisherDTO>
                //.Map(dest => dest.Street, src => src.Location.Split(',', StringSplitOptions.None)[0].Trim())
                //.Map(dest => dest.City, src => src.Location.Split(',', StringSplitOptions.None)[1].Trim())
                //.Map(dest => dest.Country, src => src.Location.Split(',', StringSplitOptions.None)[2].Trim())
                .TwoWays();

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        }
    }
}
