using CreatingSharedDatabase.Console.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreatingSharedDatabase.Console.Data.Configurations.DefaultConfiguration
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(u => u.UserId);
            builder
                .Property(u => u.UserId)
                .ValueGeneratedOnAdd();

            builder
                .Property(e => e.DateCreated)
                .HasDefaultValueSql("GETUTCDATE()");

            
            builder
                .Property(p => p.FirstName)
                .HasColumnType("NVARCHAR(50)")
                .IsRequired();
            builder
                .Property(p => p.LastName)
                .HasColumnType("NVARCHAR(50)")
                .IsRequired();
            builder
                .Property(p => p.Email)
                .HasColumnType("NVARCHAR(80)")
                .IsRequired();
            builder
                .Property(p => p.Street)
                .HasColumnType("NVARCHAR(80)")
                .IsRequired();
            builder
                .Property(p => p.City)
                .HasColumnType("NVARCHAR(50)")
                .IsRequired();
            builder
                .Property(p => p.Country)
                .HasColumnType("NVARCHAR(50)")
                .IsRequired();
            builder
                .Property(p => p.Password)
                .HasColumnType("NVARCHAR(30)")
                .IsRequired();
            builder
                .Property(p => p.Phone)
                .HasColumnType("NVARCHAR(20)")
                .IsRequired();
            
            
            builder // one-to-many  Users - Loans
                .HasMany(u => u.Loans)
                .WithOne(l => l.User)
                .HasPrincipalKey(l => l.UserId);

            builder // one-to-many  Users - Reviews
                .HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasPrincipalKey(r => r.UserId);
        }
    }
}
