using Application.Common.Pagging.Entities;
using Application.Features.BookAuthorFeatures.Queries.Common;
using MediatR;

namespace Application.Features.BookAuthorFeatures.Queries.GetAllBookAuthors
{
    public class GetAllBookAuthorsQuery : IRequest<IEnumerable<BookAuthorsDTO>>
    {
        public BookAuthorParameter _parameters { get; }

        public GetAllBookAuthorsQuery(BookAuthorParameter parameters)
        {
            _parameters = parameters;
        }
    }
}