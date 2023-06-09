using System.Reflection;
using Application;
using Application.Common.Behaviours;
using Application.System.Commands.SeedBogusData;
using Infrastructure;
using Microsoft.OpenApi.Models;
using Persistence;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

#region Swagger
{
    // Register a Swagger generator by defining 1 or more Swagger documents
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "School Library",
            Version = "v1",
            Description = "API for performing operations with \"School Library\"",
        });

        // Customizing the comment path for Swagger JSON and UI
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    });
}
#endregion

builder.Services.AddInfrastructure(configuration); // register ASP.NET Core Identity (AddAuthentication)
builder.Services.AddPersistence(configuration);
builder.Services.AddApplication();

builder.Services.AddControllers()
    .AddNewtonsoftJson(); // HATEOS | NuGet package - Microsoft.AspNetCore.Mvc.NewtonsoftJson


// DataGenerator.InitBogusData(); // Seed data
var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>(); // Custom middleware (ExceptionHandlingMiddleware)

#region Swagger
{
    if (app.Environment.IsDevelopment())
    {
        // Enable middleware to serve generated Swagger as a JSON endpoint.
        app.UseSwagger();

        // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            //options.RoutePrefix = string.Empty;
        });
    }
}
#endregion

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();