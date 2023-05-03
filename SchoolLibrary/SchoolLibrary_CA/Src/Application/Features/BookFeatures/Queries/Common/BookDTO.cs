using Application.Common.HATEOS;

namespace Application.Features.BookFeatures.Queries.Common
{
    public class BookDTO: LinkBaseEntity
    {
        public Guid BookId { get; set; }
        public string Title { get; set; } = default!;
        public int PublishingYear { get; set; }
        public string ISBN { get; set; } = default!;
        public Guid PublisherId { get; set; }
    }
}