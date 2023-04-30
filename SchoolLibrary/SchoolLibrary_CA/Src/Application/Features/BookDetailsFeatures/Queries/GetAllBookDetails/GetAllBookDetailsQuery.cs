using Application.Common.Pagging.Entities;
using Application.Features.BookDetailsFeatures.Queries.Common;
using MediatR;

namespace Application.Features.BookDetailsFeatures.Queries.GetAllBookDetails
{
    public class GetAllBookDetailsQuery : IRequest<IEnumerable<BookDetailsDTO>>
    {
        public BookDetailsParameter _parameters { get; }

        public GetAllBookDetailsQuery(BookDetailsParameter parameters)
        {
            _parameters = parameters;
        }
    }
}