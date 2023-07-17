namespace SchoolLibrary_EF.DAL.Entities
{
    public class BookDetails : BaseEntity
    {
        public Guid BookDetailId
        {
            get => Id;
            set => Id = value;
        }
        public int Pages { get; set; }
        public string Language { get; set; } = default!;
        public string Format { get; set; } = default!;


        public Guid BookId { get; set; } // FK
        public Book Book { get; set; } = default!; // one-to-one
    }
}
