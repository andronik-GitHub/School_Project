namespace SchoolLibrary_Dapper.BLL.DTOs.BookAuthorDTOs
{
    public class GetDTOInclude_BookAuthors : GetDTO_BookAuthors
    {
        public string BookTitle { get; set; } = default!;
        public string BookISBN { get; set; } = default!;
        public string AuthorName { get; set; } = default!;
    }
}