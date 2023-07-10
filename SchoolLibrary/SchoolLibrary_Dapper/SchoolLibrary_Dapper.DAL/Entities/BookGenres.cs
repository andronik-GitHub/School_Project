namespace SchoolLibrary_Dapper.DAL.Entities
{
    public class BookGenres : BaseEntity
    {
        public Guid BookId { get; set; }
        public Guid GenreId { get; set; }
    }
}
