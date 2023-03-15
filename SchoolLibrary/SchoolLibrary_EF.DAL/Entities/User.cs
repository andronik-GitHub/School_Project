using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLibrary_EF.DAL.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = default!;
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = default!;
        [Required]
        [MaxLength(80)]
        public string Email { get; set; } = default!;
        [Required]
        [MaxLength(30)]
        public string Password { get; set; } = default!;
        [Required]
        [MaxLength(100)]
        public string Address { get; set; } = default!;
        [Required]
        [MaxLength(20)]
        public string Phone { get; set; } = default!;


        public ICollection<Loan> Loans { get; set; } = default!; // one-to-many
        public ICollection<Review> Reviews { get; set; } = default!; // one-to-many
    }
}
