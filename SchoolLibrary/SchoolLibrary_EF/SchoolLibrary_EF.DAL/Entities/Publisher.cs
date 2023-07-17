namespace SchoolLibrary_EF.DAL.Entities
{
    public class Publisher : BaseEntity
    {
        public Guid PublisherId
        {
            get => Id;
            set => Id = value;
        }
        public string Name { get; set; } = default!;
        public string Location { get; set; } = default!;


        public ICollection<Book> Books { get; set; } = default!; // one-to-many
    }
}
