using Application.Common.HATEOS;

namespace Application.Features.BookDetailsFeatures.Queries.Common
{
    public class BookDetailsDTO: LinkBaseEntity
    {
        public Guid BookDetailId { get; set; }
        public int Pages { get; set; }
        public string Language { get; set; } = default!;
        public string Format { get; set; } = default!;
        public Guid BookId { get; set; }
    }
}