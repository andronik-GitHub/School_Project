using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLibrary_Dapper.DAL.Entities
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public Guid BookId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; } = default!;
        [Required]
        public int PublishingYear { get; set; }
        [Required]
        public Guid PublisherId { get; set; }
        [Required]
        [MaxLength(20)]
        public string ISBN { get; set; } = default!;
    }
}
