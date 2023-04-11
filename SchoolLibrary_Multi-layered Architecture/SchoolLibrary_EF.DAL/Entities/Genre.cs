using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLibrary_EF.DAL.Entities
{
    [Table("Genres")]
    public class Genre
    {
        [Key]
        public Guid GenreId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = default!;


        public ICollection<BookGenres> BookGenres { get; set; } = default!; // many-to-many
    }
}
