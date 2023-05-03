using Application.Common.HATEOS;

namespace Application.Features.GenreFeatures.Queries.Common
{
    public class GenreDTO: LinkBaseEntity
    {
        public Guid GenreId { get; set; }
        public string Name { get; set; } = default!;
    }
}