using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations.DefaultConfiguration
{
    public class GenresConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder
                .HasKey(g => g.GenreId);
            builder
                .Property(g => g.GenreId)
                .ValueGeneratedOnAdd();
            
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
