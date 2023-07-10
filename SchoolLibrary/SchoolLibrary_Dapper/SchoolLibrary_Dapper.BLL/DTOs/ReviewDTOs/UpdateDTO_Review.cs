namespace SchoolLibrary_Dapper.BLL.DTOs.ReviewDTOs
{
    public class UpdateDTO_Review
    {
        public Guid ReviewId { get; set; }
        public Guid UserId { get; set; }
        public decimal Rating { get; set; }
        public string ReviewText { get; set; } = default!;
    }
}