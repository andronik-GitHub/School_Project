using Application.Features.GenreFeatures.Queries.Common;
using MediatR;

namespace Application.Features.GenreFeatures.Queries.GetGenre
{
    public class GetGenreByIdQuery : IRequest<GenreDTO>
    {
        public Guid Id { get; set; } = default!;
    }
}