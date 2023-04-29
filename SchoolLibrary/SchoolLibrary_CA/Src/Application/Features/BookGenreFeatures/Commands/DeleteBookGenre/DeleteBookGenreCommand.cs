using MediatR;

namespace Application.Features.BookGenreFeatures.Commands.DeleteBookGenre
{
    public class DeleteBookGenreCommand : IRequest<(Guid, Guid)>
    {
        public Guid BookId { get; set; } = default!;
        public Guid GenreId { get; set; } = default!;
    }
}