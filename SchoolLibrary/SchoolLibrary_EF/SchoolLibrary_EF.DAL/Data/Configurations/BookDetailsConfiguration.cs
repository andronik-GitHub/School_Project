using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolLibrary_EF.DAL.Entities;

namespace SchoolLibrary_EF.DAL.Data.Configurations
{
    public class BookDetailsConfiguration : IEntityTypeConfiguration<BookDetails>
    {
        public void Configure(EntityTypeBuilder<BookDetails> builder)
        {
            builder.ToTable("BookDetails");
            
            builder.HasKey(bd => bd.BookDetailId);
            
            builder
                .Property(e => e.DateCreated)
                .HasDefaultValueSql("GETUTCDATE()");

            
            builder
                .Property(bd => bd.Pages)
                .IsRequired();
            builder
                .Property(bd => bd.Language)
                .HasColumnType("NVARCHAR(20)")
                .IsRequired();
            builder
                .Property(bd => bd.Format)
                .HasColumnType("NVARCHAR(30)")
                .IsRequired();
            
            builder
                .HasIndex(bd => bd.BookId)
                .IsUnique();
        }
    }
}
