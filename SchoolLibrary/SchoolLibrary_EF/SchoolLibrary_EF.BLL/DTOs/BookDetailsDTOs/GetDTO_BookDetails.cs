using SchoolLibrary_EF.BLL.DTOs.__HATEOAS;

namespace SchoolLibrary_EF.BLL.DTOs.BookDetailsDTOs
{
    public class GetDTO_BookDetails : BaseDTO
    {
        public Guid BookDetailId { get; set; }
        public int Pages { get; set; }
        public string Language { get; set; } = default!;
        public string BookTitle { get; set; } = default!;
        public string ISBN { get; set; } = default!;
    }
}
