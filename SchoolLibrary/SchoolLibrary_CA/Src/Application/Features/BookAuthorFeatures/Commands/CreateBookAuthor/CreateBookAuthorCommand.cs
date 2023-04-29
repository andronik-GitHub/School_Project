using MediatR;

namespace Application.Features.BookAuthorFeatures.Commands.CreateBookAuthor
{
    public class CreateBookAuthorCommand : IRequest<(Guid, Guid)>
    {
        public Guid BookId { get; set; } = default!;
        public Guid AuthorId { get; set; } = default!;
    }
}