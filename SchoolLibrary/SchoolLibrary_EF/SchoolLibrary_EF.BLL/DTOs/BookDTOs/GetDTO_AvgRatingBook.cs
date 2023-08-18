namespace SchoolLibrary_EF.BLL.DTOs.BookDTOs
{
    public class GetDTO_AvgRatingBook
    {
        public string BookTitle { get; set; } = default!;
        public decimal? Average { get; set; } = default!;
    }
}