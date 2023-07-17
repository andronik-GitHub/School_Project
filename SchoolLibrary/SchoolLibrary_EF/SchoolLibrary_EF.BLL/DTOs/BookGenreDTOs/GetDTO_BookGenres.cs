using SchoolLibrary_EF.BLL.DTOs.__HATEOAS;

namespace SchoolLibrary_EF.BLL.DTOs.BookGenreDTOs
{
    public class GetDTO_BookGenres : BaseDTO
    {
        public Guid BookId { get; set; }
        public Guid GenreId { get; set; }
        public string BookTitle { get; set; } = default!;
        public string GenreName { get; set; } = default!;
    }
}
