using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolLibrary_EF.DAL.Entities;

namespace SchoolLibrary_EF.DAL.Data.Configurations
{
    public class AuthorsConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Authors");
            
            builder.HasKey(a => a.AuthorId);
            
            builder
                .Property(e => e.DateCreated)
                .HasDefaultValueSql("GETUTCDATE()");


            builder
                .Property(a => a.FirstName)
                .HasColumnType("NVHARCHAR(50)")
                .IsRequired();
            builder
                .Property(a => a.LastName)
                .HasColumnType("NVHARCHAR(50)")
                .IsRequired();
            builder
                .Property(a => a.Birthdate)
                .IsRequired();
            builder
                .Property(a => a.Nationality)
                .HasColumnType("NVHARCHAR(30)")
                .IsRequired();
            
            builder // many-to-many  Authors - BookAuthors - Books
                .HasMany(a => a.BookAuthors)
                .WithOne(ba => ba.Author)
                .HasForeignKey(ba => ba.AuthorId);
        }
    }
}
