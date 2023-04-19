using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLibrary_EF.DAL.Entities
{
    [Table("Authors")]
    public class Author
    {
        [Key]
        public Guid AuthorId { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = default!;
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = default!;
        [Required]
        public DateTime Birthdate { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nationality { get; set; } = default!;


        public ICollection<BookAuthors> BookAuthors { get; set; } = default!; // many-to-many
    }
}
