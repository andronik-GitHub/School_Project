using System.ComponentModel.DataAnnotations;

namespace SchoolLibrary_EF.DAL.Entities.Identity
{
    public class AddRoleModel
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Role { get; set; } = default!;
    }
}