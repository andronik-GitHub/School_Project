namespace SchoolLibrary_EF.BLL.DTOs.ReviewDTOs
{
    public class UpdateDTO_Review
    {
        public Guid ReviewId { get; set; }
        public decimal Rating { get; set; }
        public string ReviewText { get; set; } = default!;
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
    }
}