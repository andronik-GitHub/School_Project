using Application.Features.BookAuthorFeatures.Queries.Common;
using MediatR;

namespace Application.Features.BookAuthorFeatures.Queries.GetBookAuthor
{
    public class GetBookAuthorByIdsQuery : IRequest<BookAuthorsDTO>
    {
        public Guid BookId { get; set; } = default!;
        public Guid AuthorId { get; set; } = default!;
    }
}