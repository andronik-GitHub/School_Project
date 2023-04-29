using Application.Features.BookGenreFeatures.Queries.Common;
using MediatR;

namespace Application.Features.BookGenreFeatures.Queries.GetAllBookGenres
{
    public class GetAllBookGenresQuery : IRequest<IEnumerable<BookGenresDTO>>
    {
    }
}