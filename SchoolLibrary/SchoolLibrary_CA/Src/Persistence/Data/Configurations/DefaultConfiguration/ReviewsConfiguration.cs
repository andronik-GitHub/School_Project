using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations.DefaultConfiguration
{
    public class ReviewsConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder
                .HasKey(r => r.ReviewId);
            builder
                .Property(r => r.ReviewId)
                .ValueGeneratedOnAdd();
            
            builder
                .Property(e => e.DateCreated)
                .HasDefaultValueSql("GETUTCDATE()");

            
            builder
                .Property(r => r.UserId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();
            builder
                .Property(r => r.BookId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();
            builder
                .Property(r => r.Rating)
                .HasColumnType("decimal(2, 1)");
            builder
                .Property(r => r.ReviewText)
                .HasColumnType("NVARCHAR(1000)")
                .IsRequired();


            builder // one-to-many  Reviews - Users
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId);

            builder // one-to-many  Reviews - Books
                .HasOne(r => r.Book)
                .WithMany(b => b.Reviews)
                .HasForeignKey(r => r.BookId);
        }
    }
}
