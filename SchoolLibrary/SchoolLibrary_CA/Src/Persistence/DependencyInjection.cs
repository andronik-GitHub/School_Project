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
            services.AddDbContext<SQLServer_SchoolLibraryContext>(options =>
            {
                options
                    .UseSqlServer(
                        configuration.GetConnectionString("sqlConnection"),
                        b => b.MigrationsAssembly(typeof(SQLServer_SchoolLibraryContext).Assembly.FullName)
                    )
                    .EnableSensitiveDataLogging();
            });

            services.AddScoped<ISchoolLibraryContext>
                (provider => provider.GetRequiredService<SQLServer_SchoolLibraryContext>());
        } 
    }
}