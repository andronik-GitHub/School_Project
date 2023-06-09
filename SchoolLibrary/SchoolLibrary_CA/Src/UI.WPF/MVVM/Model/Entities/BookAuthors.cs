﻿using System;

namespace UI.WPF.MVVM.Model.Entities
{
    public class BookAuthors  : BaseEntity // many-to-many
    {
        public Guid BookId { get; set; }
        public Book Book { get; set; } = default!;

        public Guid AuthorId { get; set; }
        public Author Author { get; set; } = default!;
    }
}
