using MediatR;

namespace Application.Features.GenreFeatures.Commands.UpdateGenre
{
    public class UpdateGenreCommand : IRequest<Guid>
    {
        public Guid Id { get; set; } = default!;
        public string Name { get; set; } = default!;
    }
}