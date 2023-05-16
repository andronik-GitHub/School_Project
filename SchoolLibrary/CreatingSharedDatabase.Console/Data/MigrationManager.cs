using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CreatingSharedDatabase.Console.Data
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<SchoolLibraryContext>())
                {
                    try
                    {
                        appContext.Database.Migrate();
                    }   
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex);
                        throw;
                    }
                }
            }
            
            return host;
        }
    }
}