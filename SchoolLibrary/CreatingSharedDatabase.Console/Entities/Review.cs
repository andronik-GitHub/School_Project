﻿namespace CreatingSharedDatabase.Console.Entities
{
    public class Review : BaseEntity
    {
        public Guid ReviewId
        {
            get => Id;
            set => Id = value;
        }
        public decimal Rating { get; set; }
        public string ReviewText { get; set; } = default!;

        public Guid UserId { get; set; }
        public User User { get; set; } = default!; // many-to-one
        public Guid BookId { get; set; }
        public Book Book { get; set; } = default!; // many-to-one
    }
}
