using System;
using System.Collections.Generic;

namespace UI.WPF.MVVM.Model.Entities
{
    public class User : BaseEntity
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Phone { get; set; } = default!;
    
        public ICollection<Loan> Loans { get; private set; } = new HashSet<Loan>(); // one-to-many
        public ICollection<Review> Reviews { get; private set; } = new HashSet<Review>(); // one-to-many

        public User() { }
    }
}
