using Application.Features.BookGenreFeatures.Queries.Common;
using MediatR;

namespace Application.Features.BookGenreFeatures.Queries.GetBookGenre
{
    public class GetBookGenreByIdsQuery : IRequest<BookGenresDTO>
    {
        public Guid BookId { get; set; } = default!;
        public Guid GenreId { get; set; } = default!;
    }
}