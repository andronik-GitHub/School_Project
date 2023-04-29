using System.Reflection;
using Application.Common.Mapping;
using Application.Common.Mapping.Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly()); // MediatR
            services.RegisterMapsterConfiguration(); // Mapster
        } 
    }
}