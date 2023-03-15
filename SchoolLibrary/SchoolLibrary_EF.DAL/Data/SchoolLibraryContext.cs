using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Data.Configurations;

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

            modelBuilder.ApplyConfiguration(new BooksConfiguration());
            modelBuilder.ApplyConfiguration(new BookDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorsConfiguration());
            modelBuilder.ApplyConfiguration(new PublishersConfiguration());
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
            modelBuilder.ApplyConfiguration(new LoansConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewsConfiguration());
            modelBuilder.ApplyConfiguration(new GenresConfiguration());
            modelBuilder.ApplyConfiguration(new BookGenresConfiguration());
            modelBuilder.ApplyConfiguration(new BookAuthorsConfiguration());
        }
    }
}
