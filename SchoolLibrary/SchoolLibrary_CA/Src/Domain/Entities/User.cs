using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public Guid UserId { get; set; }
        public UserName UserName { get; set; } = default!; // ValueObject
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public Address Address { get; set; } = default!; // ValueObject
        public string Phone { get; set; } = default!;

        public ICollection<Loan> Loans { get; private set; } = new HashSet<Loan>(); // one-to-many
        public ICollection<Review> Reviews { get; private set; } = new HashSet<Review>(); // one-to-many

        public User() { }
    }
}
