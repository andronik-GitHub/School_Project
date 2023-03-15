using Microsoft.EntityFrameworkCore;

namespace SchoolLibrary_EF.DAL.Data
{
    public class SchoolLibraryContext : DbContext
    {
        // DbSet<T> ...
        public SchoolLibraryContext(DbContextOptions options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
