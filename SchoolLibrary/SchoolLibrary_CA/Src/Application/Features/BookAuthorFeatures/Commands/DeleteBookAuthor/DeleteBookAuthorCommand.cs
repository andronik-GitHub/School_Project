using MediatR;

namespace Application.Features.BookAuthorFeatures.Commands.DeleteBookAuthor
{
    public class DeleteBookAuthorCommand : IRequest<(Guid, Guid)>
    {
        public Guid BookId { get; set; } = default!;
        public Guid AuthorId { get; set; } = default!;
    }
}