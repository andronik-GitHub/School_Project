using MediatR;

namespace Application.Features.PublisherFeatures.Commands.CreatePublisher
{
    public class CreatePublisherCommand : IRequest<Guid>
    {
        public string Name { get; set; } = default!;
        public string Location { get; set; } = default!;
        
    }
}