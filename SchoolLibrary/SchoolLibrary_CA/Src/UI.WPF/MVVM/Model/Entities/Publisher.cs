using System;
using System.Collections.Generic;

namespace UI.WPF.MVVM.Model.Entities
{
    public class Publisher : BaseEntity
    {
        public Guid PublisherId { get; set; }
        public string Name { get; set; } = default!;
        public string Location { get; set; } = default!;

        public ICollection<Book> Books { get; private set; } // one-to-many


        public Publisher()
        {
            Books = new HashSet<Book>();
        }
    }
}
