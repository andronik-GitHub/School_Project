using MediatR;

namespace Application.Features.BookAuthorFeatures.Commands.UpdateBookAuthor
{
    public class UpdateBookAuthorCommand : IRequest<(Guid, Guid)>
    {
        public Guid BookId { get; set; } = default!;
        public Guid AuthorId { get; set; } = default!;
    }
}