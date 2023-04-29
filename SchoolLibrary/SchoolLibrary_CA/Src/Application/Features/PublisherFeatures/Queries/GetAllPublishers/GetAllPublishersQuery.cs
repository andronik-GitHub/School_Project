using Application.Features.PublisherFeatures.Queries.Common;
using MediatR;

namespace Application.Features.PublisherFeatures.Queries.GetAllPublishers
{
    public class GetAllPublishersQuery : IRequest<IEnumerable<PublisherDTO>>
    {
    }
}