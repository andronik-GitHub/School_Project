using System.ComponentModel.DataAnnotations;

namespace SchoolLibrary_EF.DAL.Entities
{
    public class BookDetails
    {
        [Key]
        public Guid BookDetailId { get; set; }
        [Required]
        public int Pages { get; set; }
        [Required]
        [MaxLength(20)]
        public string Language { get; set; } = default!;
        [Required]
        [MaxLength(30)]
        public string Format { get; set; } = default!;


        public Guid BookId { get; set; } // FK
        public Book Book { get; set; } = default!; // one-to-one
    }
}
