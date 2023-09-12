using Application.Features.GenreFeatures.Commands.CreateGenre;
using Application.Features.GenreFeatures.Commands.UpdateGenre;
using Application.Features.GenreFeatures.Queries.Common;
using Domain.Entities;
using Mapster;

namespace Application.Common.Mapping.Mapster.MapsterConfiguraions
{
    public static class RegisterGenreConfig
    {
        public static void Registration()
        {
            RegisterCreateGenreCommandConfig();
            RegisterUpdateGenreCommandConfig();
            RegisterGenreDTOConfig();
            RegisterCountOfBooksEachGenreConfig();
        }

        private static void RegisterCreateGenreCommandConfig()
        {
            TypeAdapterConfig<CreateGenreCommand, Genre>
                .NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .TwoWays();
        }
        private static void RegisterUpdateGenreCommandConfig()
        {
            TypeAdapterConfig<UpdateGenreCommand, Genre>
                .NewConfig()
                .Map(dest => dest.GenreId, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .TwoWays();
        }
        private static void RegisterGenreDTOConfig()
        {
            TypeAdapterConfig<GenreDTO, Genre>
                .NewConfig()
                .Map(dest => dest.GenreId, src => src.GenreId)
                .Map(dest => dest.Name, src => src.Name)
                .TwoWays();
        }
        private static void RegisterCountOfBooksEachGenreConfig()
        {
            TypeAdapterConfig<(string GenreName, int BookCount), GetDTO_CountOfBooksEachGenre>
                .NewConfig()
                .Map(dest => dest.GenreName, src => src.GenreName)
                .Map(dest => dest.BookCount, src => src.BookCount)
                .TwoWays();
        }
    }
}