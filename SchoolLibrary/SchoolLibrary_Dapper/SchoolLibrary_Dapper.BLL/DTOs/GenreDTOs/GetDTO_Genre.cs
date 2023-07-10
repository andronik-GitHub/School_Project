namespace SchoolLibrary_Dapper.BLL.DTOs.GenreDTOs
{
    public class GetDTO_Genre
    {
        public Guid GenreId { get; set; }
        public string Name { get; set; } = default!;
    }
}
