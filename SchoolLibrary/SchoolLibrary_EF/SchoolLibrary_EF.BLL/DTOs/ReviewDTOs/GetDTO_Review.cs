using SchoolLibrary_EF.BLL.DTOs.__HATEOAS;

namespace SchoolLibrary_EF.BLL.DTOs.ReviewDTOs
{
    public class GetDTO_Review : BaseDTO
    {
        public Guid ReviewId { get; set; }
        public decimal Rating { get; set; }
        public string ReviewText { get; set; } = default!;
        public string UserFullName { get; set; } = default!;
        public string BookTitle { get; set; } = default!;
    }
}
