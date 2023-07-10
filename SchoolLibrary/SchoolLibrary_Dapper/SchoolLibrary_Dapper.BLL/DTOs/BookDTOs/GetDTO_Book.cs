namespace SchoolLibrary_Dapper.BLL.DTOs.BookDTOs
{
    public class GetDTO_Book
    {
        public Guid BookId { get; set; }
        public string Title { get; set; } = default!;
        public Guid PublisherId { get; set; }
        public string ISBN { get; set; } = default!;
    }
}
