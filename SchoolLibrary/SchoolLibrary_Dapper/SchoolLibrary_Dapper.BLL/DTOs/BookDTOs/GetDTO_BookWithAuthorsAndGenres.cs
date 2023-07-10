namespace SchoolLibrary_Dapper.BLL.DTOs.BookDTOs
{
    public class GetDTO_BookWithAuthorsAndGenres
    {
        public Guid BookId { get; set; }
        public string Title { get; set; } = default!;
        public string ISBN { get; set; } = default!;
        
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; } = default!;
        
        public Guid GenreId { get; set; }
        public string GenreName { get; set; } = default!;
    }
}