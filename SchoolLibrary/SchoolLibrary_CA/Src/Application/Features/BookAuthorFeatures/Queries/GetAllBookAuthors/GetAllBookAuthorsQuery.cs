using Application.Common.Pagging.Entities;
using Application.Features.BookAuthorFeatures.Queries.Common;
using MediatR;

namespace Application.Features.BookAuthorFeatures.Queries.GetAllBookAuthors
{
    public class GetAllBookAuthorsQuery : IRequest<IEnumerable<BookAuthorsDTO>>
    {
        public BaseParameter _parameters { get; }

        public GetAllBookAuthorsQuery(BaseParameter parameters)
        {
            _parameters = parameters;
        }
    }
}