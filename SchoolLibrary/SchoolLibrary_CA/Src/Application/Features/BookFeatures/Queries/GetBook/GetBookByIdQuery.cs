using Application.Features.BookFeatures.Queries.Common;
using MediatR;

namespace Application.Features.BookFeatures.Queries.GetBook
{
    public class GetBookByIdQuery : IRequest<BookDTO>
    {
        public Guid Id { get; set; } = default!;
    }
}