using SchoolLibrary_EF.BLL.DTOs.__HATEOAS;

namespace SchoolLibrary_EF.BLL.DTOs.UserDTOs
{
    public class GetDTO_User : BaseDTO
    {
        public Guid UserId { get; set; }
        public string UserFullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Phone { get; set; } = default!;
    }
}
