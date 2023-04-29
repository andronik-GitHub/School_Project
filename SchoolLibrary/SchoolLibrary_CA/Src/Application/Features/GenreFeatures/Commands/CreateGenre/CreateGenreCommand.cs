using MediatR;

namespace Application.Features.GenreFeatures.Commands.CreateGenre
{
    public class CreateGenreCommand : IRequest<Guid>
    {
        public string Name { get; set; } = default!;
    }
}