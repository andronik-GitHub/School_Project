namespace SchoolLibrary_EF.DAL.Entities
{
    public class Book : BaseEntity
    {
        public Guid BookId
        {
            get => Id;
            set => Id = value;
        }
        public string Title { get; set; } = default!;
        public int PublishingYear { get; set; }
        public string ISBN { get; set; } = default!;


        public Guid PublisherId { get; set; }
        public Publisher Publisher { get; set; } = default!; // many-to-one
        public BookDetails BookDetails { get; set; } = default!; // one-to-one
        public ICollection<BookGenres> BookGenres { get; set; } = default!; // many-to-many
        public ICollection<BookAuthors> BookAuthors { get; set; } = default!; // many-to-many
        public ICollection<Loan> Loans { get; set; } = default!; // one-to-many
        public ICollection<Review> Reviews { get; set; } = default!; // one-to-many
    }
}
