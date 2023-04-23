using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data.Context;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SchoolLibraryContext>(options =>
            {
                options
                    .UseSqlServer(
                        configuration.GetConnectionString("sqlConnection"),
                        b => b.MigrationsAssembly(typeof(SchoolLibraryContext).Assembly.FullName)
                    )
                    .EnableSensitiveDataLogging();
            });

            services.AddScoped<ISchoolLibraryContext>(provider => provider.GetRequiredService<SchoolLibraryContext>());
        } 
    }
}