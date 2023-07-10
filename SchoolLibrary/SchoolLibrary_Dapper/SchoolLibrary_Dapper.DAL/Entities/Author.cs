namespace SchoolLibrary_Dapper.DAL.Entities
{
    public class Author : BaseEntity
    {
        public Guid AuthorId
        {
            get => id;
            set => id = value;
        }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!; 
        public DateTime Birthdate { get; set; }
        public string Nationality { get; set; } = default!;
    }
}
