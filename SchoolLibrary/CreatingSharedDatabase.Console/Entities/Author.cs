namespace CreatingSharedDatabase.Console.Entities
{
    public class Author : BaseEntity
    {
        public Guid AuthorId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime Birthdate { get; set; }
        public string Nationality { get; set; } = default!;

        
        public ICollection<BookAuthors> BookAuthors { get; set; } // many-to-many
    }
}
