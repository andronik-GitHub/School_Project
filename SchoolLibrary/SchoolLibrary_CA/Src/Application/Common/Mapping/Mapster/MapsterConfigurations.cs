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
            RegisterBookDetailsConfig.Registration();
            RegisterBookConfig.Registration();
            RegisterBookGenreConfig.Registration();
            RegisterGenreConfig.Registration();
            RegisterLoanConfig.Registration();
            RegisterPublisherConfig.Registration();
            RegisterReviewConfig.Registration();
            RegisterUserConfig.Registration();
            
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        }
    }
}