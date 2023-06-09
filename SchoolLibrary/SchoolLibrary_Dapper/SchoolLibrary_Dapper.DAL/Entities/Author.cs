﻿namespace SchoolLibrary_Dapper.DAL.Entities
{
    public class Author
    {
        public Guid AuthorId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!; 
        public DateTime Birthdate { get; set; }
        public string Nationality { get; set; } = default!;
    }
}
