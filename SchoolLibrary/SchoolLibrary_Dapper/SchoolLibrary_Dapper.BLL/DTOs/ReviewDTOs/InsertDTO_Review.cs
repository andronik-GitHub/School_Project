namespace SchoolLibrary_Dapper.BLL.DTOs.ReviewDTOs
{
    public class InsertDTO_Review
    {
        public Guid UserId { get; set; }
        public decimal Rating { get; set; }
        public string ReviewText { get; set; } = default!;
    }
}