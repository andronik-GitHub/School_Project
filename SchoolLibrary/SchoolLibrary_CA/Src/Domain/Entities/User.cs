using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public Guid UserId { get; set; }
        public UserName UserName { get; set; } // ValueObject
        public Address Address { get; set; } // ValueObject

        public ICollection<Loan> Loans { get; private set; } // one-to-many
        public ICollection<Review> Reviews { get; private  set; } // one-to-many


        public User(UserName userName, Address address)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Address = address ?? throw new ArgumentNullException(nameof(address));
            
            Loans = new HashSet<Loan>();
            Reviews = new HashSet<Review>();
        }
    }
}
