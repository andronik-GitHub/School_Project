using Microsoft.Build.Framework;

namespace SchoolLibrary_EF.BLL.DTO.Identity
{
    public class TokenRequestModel
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}