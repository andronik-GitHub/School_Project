namespace SchoolLibrary_EF.BLL.DTOs.UserDTOs
{
    public class UpdateDTO_User
    {
        public Guid Id { get; set; }
        public Dictionary<string, string> Values { get; set; } = default!;
    }
}