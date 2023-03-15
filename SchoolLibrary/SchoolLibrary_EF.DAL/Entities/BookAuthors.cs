using System.ComponentModel.DataAnnotations;

namespace SchoolLibrary_EF.DAL.Entities
{
    public class BookAuthors // many-to-many
    {
        [Key]
        public Guid BookId { get; set; }
        public Book Book { get; set; } = default!;


        [Key]
        public Guid AuthorId { get; set; }
        public Author Author { get; set; } = default!;
    }
}
