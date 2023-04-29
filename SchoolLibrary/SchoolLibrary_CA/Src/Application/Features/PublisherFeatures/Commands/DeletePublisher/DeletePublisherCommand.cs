using MediatR;

namespace Application.Features.PublisherFeatures.Commands.DeletePublisher
{
    public class DeletePublisherCommand : IRequest<Guid>
    {
        public Guid Id { get; set; } = default!;
    }
}