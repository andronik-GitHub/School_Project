using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolLibrary_EF.DAL.Entities;

namespace SchoolLibrary_EF.DAL.Data.Configurations
{
    public class BookAuthorsConfiguration : IEntityTypeConfiguration<BookAuthors>
    {
        public void Configure(EntityTypeBuilder<BookAuthors> builder)
        {
            builder.ToTable("BookAuthors");
            
            builder.HasKey(ba => new { ba.BookId, ba.AuthorId });
            
            builder
                .Property(e => e.DateCreated)
                .HasDefaultValueSql("GETUTCDATE()");


            builder // many-to-many  Books - BookAuthors - Authors
                .HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder // many-to-many  Books - BookAuthors - Authors
                .HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
