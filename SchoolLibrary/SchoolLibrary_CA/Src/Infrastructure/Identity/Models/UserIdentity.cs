using System.Runtime.Serialization;
using Application.Features.UserIdentityFeatures.Queries.Common;
using Infrastructure.Identity.Models.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Models
{
    public class UserIdentity : IdentityUser<Guid>
    {
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateDeleted { get; set; }
        
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;

        public List<RefreshToken> RefreshTokens { get; set; } = default!;


        [IgnoreDataMember]
        public string FullName => $"{FirstName} {LastName}";
    }
}