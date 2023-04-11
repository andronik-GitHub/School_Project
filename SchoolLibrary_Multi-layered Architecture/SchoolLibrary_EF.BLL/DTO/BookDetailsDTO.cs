using SchoolLibrary_EF.BLL.DTO.HATEOAS;

namespace SchoolLibrary_EF.BLL.DTO
{
    public class BookDetailsDTO : LinkBaseEntity
    {
        public Guid BookDetailId { get; set; }
        public int Pages { get; set; }
        public string? Language { get; set; } = default!;
        public string? Format { get; set; } = default!;
        //public Guid BookId { get; set; }
        public string? BookTitle { get; set; } = default!;
        public int BookPublishingYear { get; set; }
    }
}
