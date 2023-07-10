using SchoolLibrary_Dapper.DAL.Entities;

namespace SchoolLibrary_Dapper.BLL.DTOs.BookGenreDTOs
{
    public class GetDTO_BookGenres
    {
        public Guid BookId { get; set; }
        public Guid GenreId { get; set; }
    }
}
