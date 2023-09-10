namespace SchoolLibrary_EF.BLL.DTOs.BookDTOs
{
    public class GetDTO_BookWithoutReviews
    {
        public Guid BookId { get; set; } = default!;
        public string BookTitle { get; set; } = default!;
    }
}