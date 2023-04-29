using Application.Features.BookGenreFeatures.Commands.CreateBookGenre;
using Application.Features.BookGenreFeatures.Commands.UpdateBookGenre;
using Application.Features.BookGenreFeatures.Queries.Common;
using Domain.Entities;
using Mapster;

namespace Application.Common.Mapping.Mapster.MapsterConfiguraions
{
    public static class RegisterBookGenreConfig
    {
        public static void Registration()
        {
            RegisterCreateBookGenreCommandConfig();
            RegisterUpdateBookGenreCommandConfig();
            RegisterBookGenresDTOConfig();
        }

        private static void RegisterCreateBookGenreCommandConfig()
        {
            TypeAdapterConfig<CreateBookGenreCommand, BookGenres>
                .NewConfig()
                .Map(dest => dest.BookId, src => src.BookId)
                .Map(dest => dest.GenreId, src => src.GenreId)
                .TwoWays();
        }
        private static void RegisterUpdateBookGenreCommandConfig()
        {
            TypeAdapterConfig<UpdateBookGenreCommand, BookGenres>
                .NewConfig()
                .Map(dest => dest.BookId, src => src.BookId)
                .Map(dest => dest.GenreId, src => src.GenreId)
                .TwoWays();
        }
        private static void RegisterBookGenresDTOConfig()
        {
            TypeAdapterConfig<BookGenresDTO, BookGenres>
                .NewConfig()
                .Map(dest => dest.BookId, src => src.BookId)
                .Map(dest => dest.GenreId, src => src.GenreId)
                .TwoWays();
        }
    }
}