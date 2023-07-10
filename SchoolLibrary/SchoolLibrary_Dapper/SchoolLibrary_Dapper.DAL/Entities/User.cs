using Microsoft.AspNetCore.Identity;

namespace SchoolLibrary_Dapper.DAL.Entities
{
    public class User : BaseEntity
    {
        public Guid Id { get => id; set => id = value; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;

        private string password = default!;
        public string PasswordHash
        {
            get => password;
            set
            {
                password = new PasswordHasher<User>().HashPassword(null!, value);
            }
        }
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
    }
}
