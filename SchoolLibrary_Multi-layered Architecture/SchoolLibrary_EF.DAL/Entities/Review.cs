using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLibrary_EF.DAL.Entities
{
    [Table("Reviews")]
    public class Review
    {
        [Key]
        public Guid ReviewId { get; set; }
        [Required]
        public decimal Rating { get; set; }
        [Required]
        [MaxLength(1000)]
        public string ReviewText { get; set; } = default!;


        public Guid UserId { get; set; }
        public User User { get; set; } = default!; // many-to-one
        public Guid BookId { get; set; }
        public Book Book { get; set; } = default!; // many-to-one
    }
}
