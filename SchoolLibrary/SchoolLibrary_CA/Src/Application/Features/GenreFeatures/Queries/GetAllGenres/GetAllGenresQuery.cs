using Application.Features.GenreFeatures.Queries.Common;
using MediatR;

namespace Application.Features.GenreFeatures.Queries.GetAllGenres
{
    public class GetAllGenresQuery : IRequest<IEnumerable<GenreDTO>>
    {
    }
}