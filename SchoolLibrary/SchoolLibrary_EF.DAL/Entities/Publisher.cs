using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLibrary_EF.DAL.Entities
{
    [Table("Publishers")]
    public class Publisher
    {
        [Key]
        public Guid PublisherId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = default!;
        [Required]
        [MaxLength(100)]
        public string Location { get; set; } = default!;


        public ICollection<Book> Books { get; set; } = default!; // one-to-many
    }
}
