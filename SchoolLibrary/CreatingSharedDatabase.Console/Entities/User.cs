namespace CreatingSharedDatabase.Console.Entities
{
    public class User : BaseEntity
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string Phone { get; set; } = default!;
    
        public ICollection<Loan> Loans { get; set; } // one-to-many
        public ICollection<Review> Reviews { get; set; } // one-to-many
    }
}
