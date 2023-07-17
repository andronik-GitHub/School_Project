using SchoolLibrary_EF.BLL.DTOs.__HATEOAS;

namespace SchoolLibrary_EF.BLL.DTOs.AuthorDTOs
{
    public class GetDTO_Author : BaseDTO
    {
        public Guid AuthorId
        {
            get => Id;
            set => Id = value;
        }
        public string AuthorName { get; set; } = default!;
        public DateTime Birthdate { get; set; }
    }
}
