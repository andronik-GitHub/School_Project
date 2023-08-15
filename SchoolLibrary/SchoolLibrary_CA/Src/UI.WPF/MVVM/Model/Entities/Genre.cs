using System;
using System.Collections.Generic;

namespace UI.WPF.MVVM.Model.Entities
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
