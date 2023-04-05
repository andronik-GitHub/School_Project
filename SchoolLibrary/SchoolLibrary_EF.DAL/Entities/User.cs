using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SchoolLibrary_EF.DAL.Entities
{
    [Table("Users")]
    public class User : IdentityUser<Guid>
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = default!;
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = default!;
        [MaxLength(100)]
        public string? Address { get; set; } = default!;


        public ICollection<Loan> Loans { get; set; } = default!; // one-to-many
        public ICollection<Review> Reviews { get; set; } = default!; // one-to-many
    }
}
