using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolLibrary_EF.DAL.Entities;

namespace SchoolLibrary_EF.DAL.Data.Configurations
{
    public class GenresConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder
                .HasKey(g => g.GenreId);


            builder // many-to-many  Genres - BookGenres - Books
                .HasMany(g => g.BookGenres)
                .WithOne(bg => bg.Genre)
                .HasPrincipalKey(bg => bg.GenreId);
        }
    }
}
