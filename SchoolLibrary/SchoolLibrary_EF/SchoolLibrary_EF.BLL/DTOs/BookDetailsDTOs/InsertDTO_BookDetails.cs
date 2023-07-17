namespace SchoolLibrary_EF.BLL.DTOs.BookDetailsDTOs
{
    public class InsertDTO_BookDetails
    {
        public int Pages { get; set; }
        public string Language { get; set; } = default!;
        public string Format { get; set; } = default!;
        public Guid BookId { get; set; }
    }
}