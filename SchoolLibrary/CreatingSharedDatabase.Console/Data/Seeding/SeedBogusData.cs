using CreatingSharedDatabase.Console.Entities;
using Microsoft.EntityFrameworkCore;

namespace CreatingSharedDatabase.Console.Data.Seeding
{
    public static class SeedBogusData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(DataGenerator.Books);
            modelBuilder.Entity<BookDetails>().HasData(DataGenerator.BookDetails);
            modelBuilder.Entity<Author>().HasData(DataGenerator.Authors);
            modelBuilder.Entity<Publisher>().HasData(DataGenerator.Publishers);
            modelBuilder.Entity<User>().HasData(DataGenerator.Users);
            modelBuilder.Entity<Loan>().HasData(DataGenerator.Loans);
            modelBuilder.Entity<Review>().HasData(DataGenerator.Reviews);
            modelBuilder.Entity<Genre>().HasData(DataGenerator.Genres);
            modelBuilder.Entity<BookGenres>().HasData(DataGenerator.BookGenres);
            modelBuilder.Entity<BookAuthors>().HasData(DataGenerator.BookAuthors);
        }
    }
}