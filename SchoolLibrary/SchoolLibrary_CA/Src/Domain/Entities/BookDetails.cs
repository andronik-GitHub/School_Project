using Domain.Common;

namespace Domain.Entities
{
    public class BookDetails : BaseEntity
    {
        public Guid BookDetailId { get; set; }
        public int Pages { get; set; }
        public string Language { get; set; } = default!;
        public string Format { get; set; } = default!;

        public Guid BookId { get; set; } // FK
        public Book Book { get; set; } = default!; // one-to-one
    }
}
