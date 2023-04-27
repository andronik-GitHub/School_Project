using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Configurations.DefaultConfiguration;
using Persistence.Data.Seeding;

namespace Persistence.Data.Context
{
    public class SQLServer_SchoolLibraryContext : DbContext, ISchoolLibraryContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<BookDetails> BookDetails { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookGenres> BookGenres { get; set; }
        public DbSet<BookAuthors> BookAuthors { get; set; }

        public SQLServer_SchoolLibraryContext(DbContextOptions<SQLServer_SchoolLibraryContext> options)
            : base(options)
        {
        }

        
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration
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
            // SeedBogusData.Seed(modelBuilder);
        }
    }
}