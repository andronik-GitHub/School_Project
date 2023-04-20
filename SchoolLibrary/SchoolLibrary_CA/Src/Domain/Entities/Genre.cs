using Domain.Common;

namespace Domain.Entities
{
    public class Genre : BaseEntity
    {
        public Guid GenreId { get; set; }
        public string Name { get; set; } = default!;

        public ICollection<BookGenres> BookGenres { get; private set; } // many-to-many


        public Genre()
        {
            BookGenres = new HashSet<BookGenres>();
        }
    }
}
