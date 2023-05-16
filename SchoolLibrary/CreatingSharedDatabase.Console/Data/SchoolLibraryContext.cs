using CreatingSharedDatabase.Console.Data.Configurations.DefaultConfiguration;
using CreatingSharedDatabase.Console.Entities;
using CreatingSharedDatabase.Console.Data.Seeding;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CreatingSharedDatabase.Console.Data
{
    public class SchoolLibraryContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Book> Books { get; set; } = default!;
        public DbSet<BookDetails> BookDetails { get; set; } = default!;
        public DbSet<Author> Authors { get; set; } = default!;
        public DbSet<Publisher> Publishers { get; set; } = default!;
        // public DbSet<User> Users { get; set; } = default!;
        public DbSet<Loan> Loans { get; set; } = default!;
        public DbSet<Review> Reviews { get; set; } = default!;
        public DbSet<Genre> Genres { get; set; } = default!;
        public DbSet<BookGenres> BookGenres { get; set; } = default!;
        public DbSet<BookAuthors> BookAuthors { get; set; } = default!;
        
        
        public SchoolLibraryContext(DbContextOptions<SchoolLibraryContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurations
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
            
            // Seeding
            SeedBogusData.Seed(modelBuilder);
        }
    }
}