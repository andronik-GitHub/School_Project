namespace SchoolLibrary_Dapper.DAL.Entities
{
    public class Genre
    {
        public Guid GenreId { get; set; }
        public string Name { get; set; } = default!;
    }
}
