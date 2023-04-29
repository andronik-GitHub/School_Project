using Application.Features.BookDetailsFeatures.Queries.Common;
using MediatR;

namespace Application.Features.BookDetailsFeatures.Queries.GetBookDetails
{
    public class GetBookDetailsByIdQuery : IRequest<BookDetailsDTO>
    {
        public Guid Id { get; set; }
    }
}