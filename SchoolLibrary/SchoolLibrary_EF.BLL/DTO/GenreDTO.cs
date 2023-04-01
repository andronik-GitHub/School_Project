using SchoolLibrary_EF.BLL.DTO.HATEOAS;

namespace SchoolLibrary_EF.BLL.DTO
{
    public class GenreDTO : LinkBaseEntity
    {
        public Guid GenreId { get; set; }
        public string? Name { get; set; } = default!;
        public string? Author { get; set; } = default!;
        public decimal Rating { get; set; }
    }
}
