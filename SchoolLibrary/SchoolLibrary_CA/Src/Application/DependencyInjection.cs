using System.Reflection;
using Application.Common.Helpers;
using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Domain.Entities;
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
        } 
    }
}