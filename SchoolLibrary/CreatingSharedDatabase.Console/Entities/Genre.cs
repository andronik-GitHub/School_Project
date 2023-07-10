namespace CreatingSharedDatabase.Console.Entities
{
    public class Genre : BaseEntity
    {
        public Guid GenreId
        {
            get => Id;
            set => Id = value;
        }
        public string Name { get; set; } = default!;

        
        public ICollection<BookGenres> BookGenres { get; set; } = default!; // many-to-many
    }
}
