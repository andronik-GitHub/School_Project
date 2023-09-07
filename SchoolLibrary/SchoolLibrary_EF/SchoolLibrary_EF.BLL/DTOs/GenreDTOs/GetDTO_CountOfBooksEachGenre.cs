namespace SchoolLibrary_EF.BLL.DTOs.GenreDTOs
{
    public class GetDTO_CountOfBooksEachGenre
    {
        public string GenreName { get; set; } = default!;
        public int BookCount { get; set; } = default!;
    }
}