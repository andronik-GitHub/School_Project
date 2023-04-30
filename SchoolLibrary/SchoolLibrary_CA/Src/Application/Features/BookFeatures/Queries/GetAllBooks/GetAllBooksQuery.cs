using Application.Common.Pagging.Entities;
using Application.Features.BookFeatures.Queries.Common;
using MediatR;

namespace Application.Features.BookFeatures.Queries.GetAllBooks
{
    public class GetAllBooksQuery : IRequest<IEnumerable<BookDTO>>
    {
        public BaseParameter _parameters { get; }

        public GetAllBooksQuery(BaseParameter parameters)
        {
            _parameters = parameters;
        }
    }
}