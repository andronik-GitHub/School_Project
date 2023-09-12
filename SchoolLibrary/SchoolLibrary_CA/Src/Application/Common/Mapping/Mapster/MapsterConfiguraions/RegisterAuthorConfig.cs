using Application.Features.AuthorFeatures.Commands;
using Application.Features.AuthorFeatures.Commands.CreateAuthor;
using Application.Features.AuthorFeatures.Commands.UpdateAuthor;
using Application.Features.AuthorFeatures.Queries.Common;
using Domain.Entities;
using Mapster;

namespace Application.Common.Mapping.Mapster.MapsterConfiguraions
{
    public static class RegisterAuthorConfig
    {
        public static void Registration()
        {
            RegisterCreateAuthorCommandConfig();
            RegisterUpdateAuthorCommandConfig();
            RegisterAuthorDTOConfig();
            RegisterGetAuthorWithHighestAvgBookRatingConfig();
        }

        private static void RegisterCreateAuthorCommandConfig()
        {
            TypeAdapterConfig<CreateAuthorCommand, Author>
                .NewConfig()
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.LastName, src => src.LastName)
                .Map(dest => dest.Birthdate, src => src.Birthdate)
                .Map(dest => dest.Nationality, src => src.Nationality)
                .TwoWays();
        }
        private static void RegisterUpdateAuthorCommandConfig()
        {
            TypeAdapterConfig<UpdateAuthorCommand, Author>
                .NewConfig()
                .Map(dest => dest.AuthorId, src => src.Id)
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.LastName, src => src.LastName)
                .Map(dest => dest.Birthdate, src => src.Birthdate)
                .Map(dest => dest.Nationality, src => src.Nationality)
                .TwoWays();
        }
        private static void RegisterAuthorDTOConfig()
        {
            TypeAdapterConfig<AuthorDTO, Author>
                .NewConfig()
                .Map(dest => dest.AuthorId, src => src.AuthorId)
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.LastName, src => src.LastName)
                .Map(dest => dest.Birthdate, src => src.Birthdate)
                .Map(dest => dest.Nationality, src => src.Nationality)
                .TwoWays();
        }
        private static void RegisterGetAuthorWithHighestAvgBookRatingConfig()
        {
            TypeAdapterConfig
                    <(string FirstName, string LastName, decimal AverageRating), GetDTO_AuthorWithHighestAvgBookRating>
                .NewConfig()
                .Map(dest => dest.FirstName, src => src.FirstName)
                .Map(dest => dest.LastName, src => src.LastName)
                .Map(dest => dest.AverageRating, src => src.AverageRating)
                .TwoWays();
        }
    }
}