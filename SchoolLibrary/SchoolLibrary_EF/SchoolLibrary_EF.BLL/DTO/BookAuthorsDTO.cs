using SchoolLibrary_EF.BLL.DTO.HATEOAS;

namespace SchoolLibrary_EF.BLL.DTO
{
    public class BookAuthorsDTO : LinkBaseEntity
    {
        //public Guid BookId { get; set; }
        //public Guid AuthorId { get; set; }
        public string? BookTitle { get; set; } = default!;
        public string? AuthorFullName { get; set; } = default!;


        public override string ToString()
        {
            return $"Book Title: {this.BookTitle}; Author Name: {AuthorFullName};";
        }
    }
}
