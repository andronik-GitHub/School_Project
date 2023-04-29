using Application.Features.BookFeatures.Commands.CreateBook;
using Application.Features.BookFeatures.Commands.UpdateBook;
using Application.Features.BookFeatures.Queries.Common;
using Domain.Entities;
using Mapster;

namespace Application.Common.Mapping.Mapster.MapsterConfiguraions
{
    public static class RegisterBookConfig
    {
        public static void Registration()
        {
            RegisterCreateBookCommandConfig();
            RegisterUpdateBookCommandConfig();
            RegisterBookDTOConfig();
        }

        private static void RegisterCreateBookCommandConfig()
        {
            TypeAdapterConfig<CreateBookCommand, Book>
                .NewConfig()
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.PublishingYear, src => src.PublishingYear)
                .Map(dest => dest.ISBN, src => src.ISBN)
                .Map(dest => dest.PublisherId, src => src.PublisherId)
                .TwoWays();
        }
        private static void RegisterUpdateBookCommandConfig()
        {
            TypeAdapterConfig<UpdateBookCommand, Book>
                .NewConfig()
                .Map(dest => dest.BookId, src => src.Id)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.PublishingYear, src => src.PublishingYear)
                .Map(dest => dest.ISBN, src => src.ISBN)
                .Map(dest => dest.PublisherId, src => src.PublisherId)
                .TwoWays();
        }
        private static void RegisterBookDTOConfig()
        {
            TypeAdapterConfig<BookDTO, Book>
                .NewConfig()
                .Map(dest => dest.BookId, src => src.BookId)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.PublishingYear, src => src.PublishingYear)
                .Map(dest => dest.ISBN, src => src.ISBN)
                .Map(dest => dest.PublisherId, src => src.PublisherId)
                .TwoWays();
        }
    }
}