namespace SchoolLibrary_Dapper.DAL.Entities
{
    public class BookAuthors : BaseEntity
    {
        public Guid BookId { get; set; }
        public Guid AuthorId { get; set; }
    }
}
