using Domain.Common;

namespace Domain.Entities
{
    public class Author : BaseEntity
    {
        public Guid AuthorId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime Birthdate { get; set; }
        public string Nationality { get; set; } = default!;

        public ICollection<BookAuthors> BookAuthors { get; private set; } // many-to-many


        public Author()
        {
            BookAuthors = new HashSet<BookAuthors>();
        }
    }
}
