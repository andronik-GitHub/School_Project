namespace SchoolLibrary_Dapper.BLL.DTOs.BookDetailsDTOs
{
    public class UpdateDTO_BookDetails
    {
        public Guid BookDetailId { get; set; }
        public Guid BookId { get; set; }
        public int Pages { get; set; }
        public string Language { get; set; } = default!;
        public string Format { get; set; } = default!;
    }
}