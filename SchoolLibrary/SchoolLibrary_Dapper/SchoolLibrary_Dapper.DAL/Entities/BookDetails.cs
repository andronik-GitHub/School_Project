namespace SchoolLibrary_Dapper.DAL.Entities
{
    public class BookDetails : BaseEntity
    {
        public Guid BookDetailId
        {
            get => id;
            set => id = value;
        }
        public Guid BookId { get; set; }
        public int Pages { get; set; }
        public string Language { get; set; } = default!;
        public string Format { get; set; } = default!;
    }
}
