using Application.Common.Pagging.Entities;
using Application.Features.BookGenreFeatures.Queries.Common;
using MediatR;

namespace Application.Features.BookGenreFeatures.Queries.GetAllBookGenres
{
    public class GetAllBookGenresQuery : IRequest<IEnumerable<BookGenresDTO>>
    {
        public BookGenreParameter _parameters { get; }

        public GetAllBookGenresQuery(BookGenreParameter parameters)
        {
            _parameters = parameters;
        }
    }
}