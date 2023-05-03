using Application.Common.HATEOS;

namespace Application.Features.PublisherFeatures.Queries.Common
{
    public class PublisherDTO: LinkBaseEntity
    {
        public Guid PublisherId { get; set; }
        public string Name { get; set; } = default!;
        public string Location { get; set; } = default!;
    }
}