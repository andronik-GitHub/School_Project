using CreatingSharedDatabase.Console.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatingSharedDatabase.Console.Data.Configurations.DefaultConfiguration
{
    public class BookGenresConfiguration : IEntityTypeConfiguration<BookGenres>
    {
        public void Configure(EntityTypeBuilder<BookGenres> builder)
        {
            builder
                .HasKey(bg => new { bg.BookId, bg.GenreId });
            
            builder
                .Property(e => e.DateCreated)
                .HasDefaultValueSql("GETUTCDATE()");


            builder // many-to-many  Books - BookGenres - Genres
                .HasOne(bg => bg.Book)
                .WithMany(b => b.BookGenres)
                .HasForeignKey(bg => bg.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder // many-to-many  Books - BookGenres - Genres
                .HasOne(bg => bg.Genre)
                .WithMany(g => g.BookGenres)
                .HasForeignKey(bg => bg.GenreId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
