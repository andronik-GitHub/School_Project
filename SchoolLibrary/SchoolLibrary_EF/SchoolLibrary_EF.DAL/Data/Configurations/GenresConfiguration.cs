using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolLibrary_EF.DAL.Entities;

namespace SchoolLibrary_EF.DAL.Data.Configurations
{
    public class GenresConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("Genres");
            
            builder.HasKey(g => g.GenreId);
            
            builder
                .Property(e => e.DateCreated)
                .HasDefaultValueSql("GETUTCDATE()");


            builder
                .Property(g => g.Name)
                .HasColumnType("NVARCHAR(50)")
                .IsRequired();
            

            builder // many-to-many  Genres - BookGenres - Books
                .HasMany(g => g.BookGenres)
                .WithOne(bg => bg.Genre)
                .HasPrincipalKey(bg => bg.GenreId);
        }
    }
}
