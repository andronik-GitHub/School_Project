using Application.Features.BookDetailsFeatures.Commands;
using Application.Features.BookDetailsFeatures.Commands.CreateBookDetails;
using Application.Features.BookDetailsFeatures.Commands.UpdateBookDetails;
using Application.Features.BookDetailsFeatures.Queries.Common;
using Domain.Entities;
using Mapster;

namespace Application.Common.Mapping.Mapster.MapsterConfiguraions
{
    public static class RegisterBookDetailsConfig
    {
        public static void Registration()
        {
            RegisterCreateBookDetailsCommandConfig();
            RegisterUpdateBookDetailsCommandConfig();
            RegisterBookDetailsDTOConfig();
        }

        private static void RegisterCreateBookDetailsCommandConfig()
        {
            TypeAdapterConfig<CreateBookDetailsCommand, BookDetails>
                .NewConfig()
                .Map(dest => dest.Pages, src => src.Pages)
                .Map(dest => dest.Language, src => src.Language)
                .Map(dest => dest.Format, src => src.Format)
                .Map(dest => dest.BookId, src => src.BookId)
                .TwoWays();
        }
        private static void RegisterUpdateBookDetailsCommandConfig()
        {
            TypeAdapterConfig<UpdateBookDetailsCommand, BookDetails>
                .NewConfig()
                .Map(dest => dest.BookDetailId, src => src.Id)
                .Map(dest => dest.Pages, src => src.Pages)
                .Map(dest => dest.Language, src => src.Language)
                .Map(dest => dest.Format, src => src.Format)
                .Map(dest => dest.BookId, src => src.BookId)
                .TwoWays();
        }
        private static void RegisterBookDetailsDTOConfig()
        {
            TypeAdapterConfig<BookDetailsDTO, BookDetails>
                .NewConfig()
                .Map(dest => dest.BookDetailId, src => src.BookDetailId)
                .Map(dest => dest.Pages, src => src.Pages)
                .Map(dest => dest.Language, src => src.Language)
                .Map(dest => dest.Format, src => src.Format)
                .Map(dest => dest.BookId, src => src.BookId)
                .TwoWays();
        }
    }
}