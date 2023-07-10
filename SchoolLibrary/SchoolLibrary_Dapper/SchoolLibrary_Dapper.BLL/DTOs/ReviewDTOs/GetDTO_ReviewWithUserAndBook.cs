namespace SchoolLibrary_Dapper.BLL.DTOs.ReviewDTOs
{
    public class GetDTO_ReviewWithUserAndBook
    {
        public Guid ReviewId { get; set; }   
        public decimal ReviewRating { get; set; }     

        public Guid UserId { get; set; }
        public string UserFullName { get; set; } = default!;
        
        public Guid BookId { get; set; }
        public string BookTitle { get; set; } = default!;
        public string BookISBN { get; set; } = default!;
    }
}