using Application.Common.Pagging.Entities;
using Application.Features.BookFeatures.Queries.Common;
using MediatR;

namespace Application.Features.BookFeatures.Queries.GetAllBooks
{
    public class GetAllBooksQuery : IRequest<IEnumerable<BookDTO>>
    {
        public BookParameter _parameters { get; }

        public GetAllBooksQuery(BookParameter parameters)
        {
            _parameters = parameters;
        }
    }
}