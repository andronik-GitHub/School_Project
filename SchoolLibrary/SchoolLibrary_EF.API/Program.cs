using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

#region AddMainServices
{
    builder.Services.AddDbContext<SchoolLibraryContext>(options =>
    {
        options.UseSqlServer(
                builder.Configuration.GetConnectionString("sqlConnection"),
                options => options.MigrationsAssembly("SchoolLibrary_EF.API")
            );
    });
}
#endregion


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}


app.MapControllers();


app.Run();
