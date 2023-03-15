using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolLibrary_EF.DAL.Entities;

namespace SchoolLibrary_EF.DAL.Data.Configurations
{
    public class BookDetailsConfiguration : IEntityTypeConfiguration<BookDetails>
    {
        public void Configure(EntityTypeBuilder<BookDetails> builder)
        {
            builder
                .HasKey(bd => bd.BookDetailId);


            builder // one-to-one  BookDetails - Books
                .HasOne(bd => bd.Book)
                .WithOne(b => b.BookDetails)
                .HasForeignKey<Book>(bd => bd.BookId);
        }
    }
}
