using System.Reflection;
using Application.Common.Behaviours;
using Application.Common.Helpers;
using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly()); // MediatR
            
            // ValidationBehavior registration
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            // add the ExceptionHandlingMiddleware in the ConfigureServices
            services.AddTransient<ExceptionHandlingMiddleware>();
            // Add the validators that implemented using FluentValidation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            
            services.RegisterMapsterConfiguration(); // Mapster
            
            // HATEOAS
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(x =>
            {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext!);
            });

            // For Sorting
            services.AddScoped<ISortHelper<Author>, SortHelper<Author>>();
            services.AddScoped<ISortHelper<BookAuthors>, SortHelper<BookAuthors>>();
            services.AddScoped<ISortHelper<BookDetails>, SortHelper<BookDetails>>();
            services.AddScoped<ISortHelper<BookGenres>, SortHelper<BookGenres>>();
            services.AddScoped<ISortHelper<Book>, SortHelper<Book>>();
            services.AddScoped<ISortHelper<Genre>, SortHelper<Genre>>();
            services.AddScoped<ISortHelper<Loan>, SortHelper<Loan>>();
            services.AddScoped<ISortHelper<Publisher>, SortHelper<Publisher>>();
            services.AddScoped<ISortHelper<Review>, SortHelper<Review>>();
            services.AddScoped<ISortHelper<User>, SortHelper<User>>();

            // For DataShaping
            services.AddScoped<IDataShaper<Author>, DataShaper<Author>>();
            services.AddScoped<IDataShaper<BookAuthors>, DataShaper<BookAuthors>>();
            services.AddScoped<IDataShaper<BookDetails>, DataShaper<BookDetails>>();
            services.AddScoped<IDataShaper<BookGenres>, DataShaper<BookGenres>>();
            services.AddScoped<IDataShaper<Book>, DataShaper<Book>>();
            services.AddScoped<IDataShaper<Genre>, DataShaper<Genre>>();
            services.AddScoped<IDataShaper<Loan>, DataShaper<Loan>>();
            services.AddScoped<IDataShaper<Publisher>, DataShaper<Publisher>>();
            services.AddScoped<IDataShaper<Review>, DataShaper<Review>>();
            services.AddScoped<IDataShaper<User>, DataShaper<User>>();
        } 
    }
}