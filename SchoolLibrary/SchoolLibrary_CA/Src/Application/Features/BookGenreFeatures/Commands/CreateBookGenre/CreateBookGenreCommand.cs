using MediatR;

namespace Application.Features.BookGenreFeatures.Commands.CreateBookGenre
{
    public class CreateBookGenreCommand : IRequest<(Guid, Guid)>
    {
        public Guid BookId { get; set; } = default!;
        public Guid GenreId { get; set; } = default!;
    }
}