namespace SchoolLibrary_EF.DAL.Entities
{
    public class Author : BaseEntity
    {
        public Guid AuthorId
        {
            get => Id;
            set => Id = value;
        }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime Birthdate { get; set; }
        public string Nationality { get; set; } = default!;


        public ICollection<BookAuthors> BookAuthors { get; set; } = default!; // many-to-many
    }
}
