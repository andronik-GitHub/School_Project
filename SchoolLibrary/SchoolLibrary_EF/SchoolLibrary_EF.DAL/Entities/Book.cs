using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolLibrary_EF.DAL.Entities
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
        [MaxLength(20)]
        public string ISBN { get; set; } = default!;


        public BookDetails BookDetails { get; set; } = default!; // one-to-one
        public Guid PublisherId { get; set; }
        public Publisher Publisher { get; set; } = default!; // many-to-one
        public ICollection<BookGenres> BookGenres { get; set; } = default!; // many-to-many
        public ICollection<BookAuthors> BookAuthors { get; set; } = default!; // many-to-many
        public ICollection<Loan> Loans { get; set; } = default!; // one-to-many
        public ICollection<Review> Reviews { get; set; } = default!; // one-to-many
    }
}
