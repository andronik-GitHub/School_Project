using Application.Common.Pagging.Entities;
using Application.Features.PublisherFeatures.Queries.Common;
using MediatR;

namespace Application.Features.PublisherFeatures.Queries.GetAllPublishers
{
    public class GetAllPublishersQuery : IRequest<IEnumerable<PublisherDTO>>
    {
        public BaseParameter _parameters { get; }

        public GetAllPublishersQuery(BaseParameter parameters)
        {
            _parameters = parameters;
        }
    }
}