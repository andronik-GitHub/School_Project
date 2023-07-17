using Microsoft.AspNetCore.Identity;
using SchoolLibrary_EF.DAL.Entities.Identity;

namespace SchoolLibrary_EF.DAL.Entities
{
    public class User : IdentityUser<Guid>
    {
        public DateTime DateCreated { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        
        
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;

        
        public List<RefreshToken>? RefreshTokens { get; set; }
        public ICollection<Loan> Loans { get; set; } = default!; // one-to-many
        public ICollection<Review> Reviews { get; set; } = default!; // one-to-many
    }
}
