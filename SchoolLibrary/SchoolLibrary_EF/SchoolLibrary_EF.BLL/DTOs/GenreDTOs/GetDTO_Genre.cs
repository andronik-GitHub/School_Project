using SchoolLibrary_EF.BLL.DTOs.__HATEOAS;

namespace SchoolLibrary_EF.BLL.DTOs.GenreDTOs
{
    public class GetDTO_Genre : BaseDTO
    {
        public Guid GenreId { get; set; }
        public string Name { get; set; } = default!;
    }
}
