namespace SchoolLibrary_Dapper.BLL.DTOs.UserDTOs
{
    public class GetDTO_User
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Address { get; set; } = default!;
    }
}
