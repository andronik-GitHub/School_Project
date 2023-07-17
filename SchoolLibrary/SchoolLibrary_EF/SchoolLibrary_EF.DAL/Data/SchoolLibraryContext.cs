using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolLibrary_EF.DAL.Bogus;
using SchoolLibrary_EF.DAL.Data.Configurations;
using SchoolLibrary_EF.DAL.Entities;

namespace SchoolLibrary_EF.DAL.Data
{
    public class SchoolLibraryContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Book> Books { get; set; } = default!;
        public DbSet<BookDetails> BookDetails { get; set; } = default!;
        public DbSet<Author> Authors { get; set; } = default!;
        public DbSet<Publisher> Publishers { get; set; } = default!;
        // public DbSet<User> Users { get; set; }
        public DbSet<Loan> Loans { get; set; } = default!;
        public DbSet<Review> Reviews { get; set; } = default!;
        public DbSet<Genre> Genres { get; set; } = default!;
        public DbSet<BookGenres> BookGenres { get; set; } = default!;
        public DbSet<BookAuthors> BookAuthors { get; set; } = default!;

        public SchoolLibraryContext(
            DbContextOptions<SchoolLibraryContext> options)
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
            
            // Seeding data
            // Seeding IdentityEntities
            // modelBuilder.Entity<IdentityRole<Guid>>().HasData(DataGenerator.Roles);
            // modelBuilder.Entity<User>().HasData(DataGenerator.Users);
            // modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(DataGenerator.UsersRoles);
            //
            // modelBuilder.Entity<Book>().HasData(DataGenerator.Books);
            // modelBuilder.Entity<BookDetails>().HasData(DataGenerator.BookDetails);
            // modelBuilder.Entity<Author>().HasData(DataGenerator.Authors);
            // modelBuilder.Entity<Publisher>().HasData(DataGenerator.Publishers);
            // modelBuilder.Entity<Loan>().HasData(DataGenerator.Loans);
            // modelBuilder.Entity<Review>().HasData(DataGenerator.Reviews);
            // modelBuilder.Entity<Genre>().HasData(DataGenerator.Genres);
            // modelBuilder.Entity<BookGenres>().HasData(DataGenerator.BookGenres);
            // modelBuilder.Entity<BookAuthors>().HasData(DataGenerator.BookAuthors);
        }
    }
}
