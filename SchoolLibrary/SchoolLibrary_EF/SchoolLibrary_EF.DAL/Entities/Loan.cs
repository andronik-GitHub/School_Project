using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLibrary_EF.DAL.Entities
{
    [Table("Loans")]
    public class Loan
    {
        [Key]
        public Guid LoanId { get; set; }
        [Required]
        public DateTime LoanDate { get; set; }
        [Required]
        public DateTime ReturnDate { get; set;}


        public Guid UserId { get; set; }
        public User User { get; set; } = default!; // many-to-one
        public Guid BookId { get; set; }
        public Book Book { get; set; } = default!; // many-to-one
    }
}
