using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity.Models
{
    public class IdentitySchoolLibraryDbContext : IdentityDbContext<UserIdentity, IdentityRole<Guid>, Guid>
    {
        public IdentitySchoolLibraryDbContext(DbContextOptions<IdentitySchoolLibraryDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserIdentityConfiguration());
        }
    }
}