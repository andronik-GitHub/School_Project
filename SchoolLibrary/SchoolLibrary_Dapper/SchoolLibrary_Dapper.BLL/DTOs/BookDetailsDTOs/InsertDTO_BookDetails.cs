namespace SchoolLibrary_Dapper.BLL.DTOs.BookDetailsDTOs
{
    public class InsertDTO_BookDetails
    {
        public Guid BookId { get; set; }
        public int Pages { get; set; }
        public string Language { get; set; } = default!;
        public string Format { get; set; } = default!;
    }
}