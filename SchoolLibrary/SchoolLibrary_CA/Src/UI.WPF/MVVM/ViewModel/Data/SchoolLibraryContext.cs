using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UI.WPF.MVVM.Model.Entities;
using UI.WPF.MVVM.ViewModel.Data.Configurations.DefaultConfiguration;

namespace UI.WPF.MVVM.ViewModel.Data
{
    public class SchoolLibraryContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Book> Books { get; set; } = default!;
        public DbSet<BookDetails> BookDetails { get; set; } = default!;
        public DbSet<Author> Authors { get; set; } = default!;
        public DbSet<Publisher> Publishers { get; set; } = default!;
        //public DbSet<User> Users { get; set; } = default!;
        public DbSet<Loan> Loans { get; set; } = default!;
        public DbSet<Review> Reviews { get; set; } = default!;
        public DbSet<Genre> Genres { get; set; } = default!;
        public DbSet<BookGenres> BookGenres { get; set; } = default!;
        public DbSet<BookAuthors> BookAuthors { get; set; } = default!;

        
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=ANDRONIK--PC;Database=SchoolLibrary;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;MultipleActiveResultSets=True;");
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
        }
    }
}