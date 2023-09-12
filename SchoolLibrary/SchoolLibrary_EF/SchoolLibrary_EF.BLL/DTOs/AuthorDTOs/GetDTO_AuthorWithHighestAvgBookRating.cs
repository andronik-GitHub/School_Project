namespace SchoolLibrary_EF.BLL.DTOs.AuthorDTOs
{
    public class GetDTO_AuthorWithHighestAvgBookRating
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public decimal AverageRating { get; set; } = default!;
    }
}