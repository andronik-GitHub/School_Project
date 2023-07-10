namespace SchoolLibrary_Dapper.BLL.DTOs.ReviewDTOs
{
    public class GetDTO_Review
    {
        public Guid ReviewId { get; set; }
        public Guid UserId { get; set; }
        public decimal Rating { get; set; }
    }
}
