using Mapster;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.DAL.Entities;
using System.Reflection;

namespace SchoolLibrary_EF.API.Mapping.Configurations
{
    public static class MapsterConfiguration
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            /*
             * При <PublisherDTO, Publisher>
             Якщо з БД витягувати об'єкти то получається:
                {
                    "publisherId": "117e5850-cfb4-4a2e-a21f-033035fe68fb",
                    "name": "Quigley, Spinka and Sawayn",
                    "country": null,
                    "city": null,
                    "street": null
                }
            а якщо робити вставку то ніяких помилок не виходить
            * При <Publisher, PublisherDTO>
            Якщо з БД витягувати об'єкти то ніяких помилок,
            а якщо робити вставку то вилітає Exception про вставку значень null в бд
            Пробував додати [Required] до всіх полів але це нічого не дало
             */
            TypeAdapterConfig<PublisherDTO, Publisher>
                .NewConfig()
                .Map(dest => dest.Location, src => $"{src.Street}, {src.City}, {src.Country}")
                // <Publisher, PublisherDTO>
                //.Map(dest => dest.Street, src => src.Location.Split(',', StringSplitOptions.None)[0].Trim())
                //.Map(dest => dest.City, src => src.Location.Split(',', StringSplitOptions.None)[1].Trim())
                //.Map(dest => dest.Country, src => src.Location.Split(',', StringSplitOptions.None)[2].Trim())
                .TwoWays();

            TypeAdapterConfig<Book, BookDTO>
                .NewConfig()
                .Map(dest => dest.PublisherName, src => src.Publisher.Name)
                .Map(dest => dest.PublisherLocation, src => src.Publisher.Location)
                .TwoWays();

            TypeAdapterConfig<BookDetails, BookDetailsDTO>
                .NewConfig()
                .Map(dest => dest.BookTitle, src => src.Book.Title)
                .Map(dest => dest.BookPublishingYear, src => src.Book.PublishingYear)
                .TwoWays();

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        }
    }
}
