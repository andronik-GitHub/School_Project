using Application.Common.Pagging.Entities;
using Application.Features.BookGenreFeatures.Queries.Common;
using MediatR;

namespace Application.Features.BookGenreFeatures.Queries.GetAllBookGenres
{
    public class GetAllBookGenresQuery : IRequest<IEnumerable<BookGenresDTO>>
    {
        public BaseParameter _parameters { get; }

        public GetAllBookGenresQuery(BaseParameter parameters)
        {
            _parameters = parameters;
        }
    }
}