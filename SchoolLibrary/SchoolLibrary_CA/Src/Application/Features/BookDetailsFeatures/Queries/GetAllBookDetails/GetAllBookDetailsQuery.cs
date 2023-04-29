using Application.Features.BookDetailsFeatures.Queries.Common;
using MediatR;

namespace Application.Features.BookDetailsFeatures.Queries.GetAllBookDetails
{
    public class GetAllBookDetailsQuery : IRequest<IEnumerable<BookDetailsDTO>>
    {
    }
}