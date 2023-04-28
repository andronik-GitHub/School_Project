using System.Reflection;
using Application.Common.Mapping;
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