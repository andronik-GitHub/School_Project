using System.ComponentModel.DataAnnotations;

namespace SchoolLibrary_EF.DAL.Entities
{
    public class BookGenres // many-to-many
    {
        [Key]
        public Guid BookId { get; set; }
        public Book Book { get; set; } = default!;


        [Key]
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; } = default!;
    }
}
