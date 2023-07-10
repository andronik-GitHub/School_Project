namespace SchoolLibrary_Dapper.BLL.DTOs.BookGenreDTOs
{
    public class GetDTOInclude_BookGenres : GetDTO_BookGenres
    {
        public string Title { get; set; } = default!;
        public string ISBN { get; set; } = default!;
        public string GenreName { get; set; } = default!;
    }
}