using MediatR;

namespace Application.Features.BookGenreFeatures.Commands.UpdateBookGenre
{
    public class UpdateBookGenreCommand : IRequest<(Guid, Guid)>
    {
        public Guid BookId { get; set; } = default!;
        public Guid GenreId { get; set; } = default!;
    }
}