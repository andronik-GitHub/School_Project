using CreatingSharedDatabase.Console.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatingSharedDatabase.Console.Data.Configurations.DefaultConfiguration
{
    public class BooksConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
                .HasKey(book => book.BookId);
            builder
                .Property(book => book.BookId)
                .ValueGeneratedOnAdd();
            
            builder
                .Property(e => e.DateCreated)
                .HasDefaultValueSql("GETUTCDATE()");


            builder
                .Property(b => b.Title)
                .HasColumnType("NVARCHAR(100)")
                .IsRequired();
            builder
                .Property(b => b.PublishingYear)
                .HasColumnType("INT")
                .IsRequired();
            builder
                .Property(b => b.PublisherId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();
            builder
                .Property(b => b.ISBN)
                .HasColumnType("NVARCHAR(20)")
                .IsRequired();
            

            builder // many-to-many  Books - BookGenres - Genres
                .HasMany(b => b.BookGenres)
                .WithOne(bg => bg.Book)
                .HasForeignKey(bg => bg.BookId);

            builder // many-to-many  Books - BookAuthors - Authors
                .HasMany(b => b.BookAuthors)
                .WithOne(ba => ba.Book)
                .HasForeignKey(ba => ba.BookId);

            builder // one-to-many  Books - Loans
                .HasMany(b => b.Loans)
                .WithOne(l => l.Book)
                .HasForeignKey(l => l.BookId);

            builder // one-to-many  Books - Reviews
                .HasMany(b => b.Reviews)
                .WithOne(r => r.Book)
                .HasForeignKey(r => r.BookId);

            builder // many-to-one  Books - Publishers
                .HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherId);

            builder // one-to-one  BookDetails - Books
                .HasOne(b => b.BookDetails)
                .WithOne(bd => bd.Book)
                .HasForeignKey<BookDetails>(bd => bd.BookId);
        }
    }
}
