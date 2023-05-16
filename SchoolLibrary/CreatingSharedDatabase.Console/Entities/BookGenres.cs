namespace CreatingSharedDatabase.Console.Entities
{
    public class BookGenres : BaseEntity // many-to-many
    {
        public Guid BookId { get; set; }
        public Book Book { get; set; } = default!;

        public Guid GenreId { get; set; }
        public Genre Genre { get; set; } = default!;
    }
}
