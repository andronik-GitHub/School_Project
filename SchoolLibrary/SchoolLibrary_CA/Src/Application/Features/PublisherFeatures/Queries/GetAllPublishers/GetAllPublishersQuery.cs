using Application.Common.Pagging.Entities;
using Application.Features.PublisherFeatures.Queries.Common;
using MediatR;

namespace Application.Features.PublisherFeatures.Queries.GetAllPublishers
{
    public class GetAllPublishersQuery : IRequest<IEnumerable<PublisherDTO>>
    {
        public PublisherParameter _parameters { get; }

        public GetAllPublishersQuery(PublisherParameter parameters)
        {
            _parameters = parameters;
        }
    }
}