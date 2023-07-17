namespace SchoolLibrary_EF.BLL.DTOs.BookDTOs
{
    public class InsertDTO_Book
    {
        public string Title { get; set; } = default!;
        public int PublishingYear { get; set; }
        public string ISBN { get; set; } = default!;
        public Guid PublisherId { get; set; }
    }
}