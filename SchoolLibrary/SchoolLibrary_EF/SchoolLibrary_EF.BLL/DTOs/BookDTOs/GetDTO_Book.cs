using SchoolLibrary_EF.BLL.DTOs.__HATEOAS;

namespace SchoolLibrary_EF.BLL.DTOs.BookDTOs
{
    public class GetDTO_Book : BaseDTO
    {
        public Guid BookId { get; set; }
        public string Title { get; set; } = default!;
        public string ISBN { get; set; } = default!;
        public string? PublisherName { get; set; } = default!;
        public string? PublisherLocation { get; set; } = default!;
    }
}
