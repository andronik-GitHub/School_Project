using MediatR;

namespace Application.Features.GenreFeatures.Commands.DeleteGenre
{
    public class DeleteGenreCommand : IRequest<Guid>
    {
        public Guid Id { get; set; } = default!;
    }
}