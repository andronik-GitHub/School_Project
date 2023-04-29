using Application.Features.GenreFeatures.Commands.CreateGenre;
using Application.Features.GenreFeatures.Commands.UpdateGenre;
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
    }
}