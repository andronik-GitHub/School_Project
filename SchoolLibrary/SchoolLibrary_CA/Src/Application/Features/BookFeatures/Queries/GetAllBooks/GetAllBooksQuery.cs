using Application.Features.BookFeatures.Queries.Common;
using MediatR;

namespace Application.Features.BookFeatures.Queries.GetAllBooks
{
    public class GetAllBooksQuery : IRequest<IEnumerable<BookDTO>>
    {
    }
}