namespace CreatingSharedDatabase.Console.Entities
{
    public class Publisher : BaseEntity
    {
        public Guid PublisherId { get; set; }
        public string Name { get; set; } = default!;
        public string Location { get; set; } = default!;

        public ICollection<Book> Books { get; set; } = default!; // one-to-many
    }
}
