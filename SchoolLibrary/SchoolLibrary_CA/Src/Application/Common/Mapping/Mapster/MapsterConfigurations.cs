using System.Reflection;
using Application.Common.Mapping.Mapster.MapsterConfiguraions;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Common.Mapping.Mapster
{
    public static class MapsterConfigurations
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            RegisterReviewConfig.Registration();
            RegisterUserConfig.Registration();
            
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        }
    }
}