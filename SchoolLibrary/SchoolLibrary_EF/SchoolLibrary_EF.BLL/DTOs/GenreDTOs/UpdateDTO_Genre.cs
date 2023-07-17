namespace SchoolLibrary_EF.BLL.DTOs.GenreDTOs
{
    public class UpdateDTO_Genre
    {
        public Guid GenreId { get; set; }
        public string Name { get; set; } = default!;
    }
}