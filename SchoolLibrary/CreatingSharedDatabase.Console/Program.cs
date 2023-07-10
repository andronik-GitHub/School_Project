using System.Text.Json;
using System.Text.Json.Serialization;
using CreatingSharedDatabase.Console.Data;
using CreatingSharedDatabase.Console.Data.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


DataGenerator.InitBogusData(); // seeding dbcontext
// GenericHost
using IHost host = Host.CreateDefaultBuilder()
    .ConfigureAppConfiguration((context, builder) =>
    {
        // Setting file "appsettings.json"
        builder.SetBasePath(AppContext.BaseDirectory);
        builder.AddJsonFile("appsettings.json");
    })
    .ConfigureServices((context, services) =>
    {
        // DbContext
        services.AddDbContext<SchoolLibraryContext>(options =>
        {
            options
                .UseSqlServer(
                    context.Configuration.GetConnectionString("sqlConnection"),
                    b => b.MigrationsAssembly(typeof(SchoolLibraryContext).Assembly.GetName().Name)
                )
                .EnableSensitiveDataLogging();
        });
    })
    .Build()
    .MigrateDatabase(); // auto-migrations after running application


using (IServiceScope serviceScope = host.Services.CreateScope())
{
    var provider = serviceScope.ServiceProvider;
    
    try
    {
        var dbcontext = provider.GetRequiredService<SchoolLibraryContext>();
        dbcontext.Database.Migrate();
        
        string json = JsonSerializer.Serialize(dbcontext.Users.Take(100).ToList(), new JsonSerializerOptions
        {
            WriteIndented = true, // spaces are included in json (relatively speaking, for beauty)
            ReferenceHandler = ReferenceHandler.Preserve // "Preserve" to avoid circular references
        });
        Console.WriteLine(json);
        
        Console.WriteLine($"Count: {dbcontext.Users.Count()}");
    }
    catch (Exception ex)
    {
        Console.WriteLine(DateTime.UtcNow + "=>" + "Request to database... Something went wrong: " + ex.Message);

    }
}
    
Console.ReadKey();