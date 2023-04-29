using Application.Features.PublisherFeatures.Queries.Common;
using MediatR;

namespace Application.Features.PublisherFeatures.Queries.GetPublisher
{
    public class GetPublisherByIdQuery : IRequest<PublisherDTO>
    {
        public Guid Id { get; set; }
    }
}