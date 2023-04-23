using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations.DefaultConfiguration
{
    public class BookDetailsConfiguration : IEntityTypeConfiguration<BookDetails>
    {
        public void Configure(EntityTypeBuilder<BookDetails> builder)
        {
            builder
                .HasKey(bd => bd.BookDetailId);
            builder
                .Property(bd => bd.BookDetailId)
                .ValueGeneratedOnAdd();
            
            builder
                .Property(e => e.DateCreated)
                .HasDefaultValueSql("GETUTCDATE()");


            builder
                .Property(bd => bd.BookId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();
            builder
                .Property(bd => bd.Pages)
                .HasColumnType("INT")
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
