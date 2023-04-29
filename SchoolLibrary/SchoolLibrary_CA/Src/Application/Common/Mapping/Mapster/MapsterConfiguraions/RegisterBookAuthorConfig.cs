using Application.Features.BookAuthorFeatures.Commands;
using Application.Features.BookAuthorFeatures.Commands.CreateBookAuthor;
using Application.Features.BookAuthorFeatures.Commands.UpdateBookAuthor;
using Application.Features.BookAuthorFeatures.Queries.Common;
using Domain.Entities;
using Mapster;

namespace Application.Common.Mapping.Mapster.MapsterConfiguraions
{
    public class RegisterBookAuthorConfig
    {
        public static void Registration()
        {
            RegisterCreateBookAuthorCommandConfig();
            RegisterUpdateBookAuthorCommandConfig();
            RegisterBookAuthorsDTOConfig();
        }

        private static void RegisterCreateBookAuthorCommandConfig()
        {
            TypeAdapterConfig<CreateBookAuthorCommand, BookAuthors>
                .NewConfig()
                .Map(dest => dest.BookId, src => src.BookId)
                .Map(dest => dest.AuthorId, src => src.AuthorId)
                .TwoWays();
        }
        private static void RegisterUpdateBookAuthorCommandConfig()
        {
            TypeAdapterConfig<UpdateBookAuthorCommand, BookAuthors>
                .NewConfig()
                .Map(dest => dest.BookId, src => src.BookId)
                .Map(dest => dest.AuthorId, src => src.AuthorId)
                .TwoWays();
        }
        private static void RegisterBookAuthorsDTOConfig()
        {
            TypeAdapterConfig<BookAuthorsDTO, BookAuthors>
                .NewConfig()
                .Map(dest => dest.BookId, src => src.BookId)
                .Map(dest => dest.AuthorId, src => src.AuthorId)
                .TwoWays();
        }
    }
}