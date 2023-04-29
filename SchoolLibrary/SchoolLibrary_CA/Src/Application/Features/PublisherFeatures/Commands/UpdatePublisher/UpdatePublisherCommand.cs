using MediatR;

namespace Application.Features.PublisherFeatures.Commands.UpdatePublisher
{
    public class UpdatePublisherCommand : IRequest<Guid>
    {
        public Guid Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Location { get; set; } = default!;
    }
}