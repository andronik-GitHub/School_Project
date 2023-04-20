using Domain.Common;

namespace Domain.Entities
{
    public class Book : BaseEntity
    {
        public Guid BookId { get; set; }
        public string Title { get; set; } = default!;
        public int PublishingYear { get; set; }
        public string ISBN { get; set; } = default!;

        public BookDetails BookDetails { get; set; } = default!; // one-to-one
        public Guid PublisherId { get; set; }
        public Publisher Publisher { get; set; } = default!; // many-to-one
        
        public ICollection<BookGenres> BookGenres { get; private set; } // many-to-many
        public ICollection<BookAuthors> BookAuthors { get; private set; } // many-to-many
        public ICollection<Loan> Loans { get; private set; } // one-to-many
        public ICollection<Review> Reviews { get; private set; } // one-to-many


        public Book()
        {
            BookGenres = new HashSet<BookGenres>();
            BookAuthors = new HashSet<BookAuthors>();
            Loans = new HashSet<Loan>();
            Reviews = new HashSet<Review>();
        }
    }
}
