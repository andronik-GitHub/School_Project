using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Identity;

namespace UI.WPF.MVVM.Model.Entities
{
    public class User : IdentityUser<Guid>
    {
        public User() { }
        
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }
        
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;

        public List<RefreshToken> RefreshTokens { get; set; } = default!;


        public ICollection<Loan> Loans { get; private set; } = new HashSet<Loan>(); // one-to-many
        public ICollection<Review> Reviews { get; private set; } = new HashSet<Review>(); // one-to-many

        [IgnoreDataMember]
        public string FullName => $"{FirstName} {LastName}";
    }
}
