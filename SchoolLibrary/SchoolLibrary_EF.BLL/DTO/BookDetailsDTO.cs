namespace SchoolLibrary_EF.BLL.DTO
{
    public class BookDetailsDTO
    {
        public Guid BookDetailId { get; set; }
        public int Pages { get; set; }
        public string Language { get; set; } = default!;
        public string Format { get; set; } = default!;
        public Guid BookId { get; set; }
    }
}
