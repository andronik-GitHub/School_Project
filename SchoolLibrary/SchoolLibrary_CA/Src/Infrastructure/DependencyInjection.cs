using Infrastructure.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentitySchoolLibraryDbContext>(options =>
            {
                options
                    .UseSqlServer(
                        configuration.GetConnectionString("sqlConnection"),
                        b => b.MigrationsAssembly(typeof(IdentitySchoolLibraryDbContext).Assembly.GetName().Name)
                    )
                    .EnableSensitiveDataLogging();
            });
        }
    }
}