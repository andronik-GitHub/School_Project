using SchoolLibrary_EF.BLL.DTOs.__HATEOAS;

namespace SchoolLibrary_EF.BLL.DTOs.BookAuthorDTOs
{
    public class GetDTO_BookAuthors : BaseDTO
    {
        public Guid BookId { get; set; }
        public Guid AuthorId { get; set; }
        public string? BookTitle { get; set; } = default!;
        public string? AuthorFullName { get; set; } = default!;
    }
}
