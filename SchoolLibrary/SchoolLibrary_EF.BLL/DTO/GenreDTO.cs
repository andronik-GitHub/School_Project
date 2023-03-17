namespace SchoolLibrary_EF.BLL.DTO
{
    public class GenreDTO
    {
        public Guid GenreId { get; set; }
        public string Name { get; set; } = default!;
        public string Author { get; set; } = default!;
        public decimal Rating { get; set; }
    }
}
