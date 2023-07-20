using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolLibrary_EF.DAL.Entities;

namespace SchoolLibrary_EF.DAL.Data.Configurations
{
    public class PublishersConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.ToTable("Publishers");
            
            builder.HasKey(p => p.PublisherId);
            
            builder
                .Property(e => e.DateCreated)
                .HasDefaultValueSql("GETUTCDATE()");


            builder
                .Property(p => p.Name)
                .HasColumnType("NVARCHAR(50)")
                .IsRequired();
            builder
                .Property(p => p.Location)
                .HasColumnType("NVARCHAR(100)")
                .IsRequired();
            

            builder // one-to-many  Publisher - Books
                .HasMany(p => p.Books)
                .WithOne(b => b.Publisher)
                .HasForeignKey(b => b.PublisherId);
        }
    }
}
