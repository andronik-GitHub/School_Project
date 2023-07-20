using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolLibrary_EF.DAL.Entities;

namespace SchoolLibrary_EF.DAL.Data.Configurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("AspNetUsers");
            
            builder.HasKey(u => u.Id);
            
            builder
                .Property(e => e.DateCreated)
                .HasDefaultValueSql("GETUTCDATE()");


            builder
                .Property(u => u.FirstName)
                .HasColumnType("NVARCHAR(50)")
                .IsRequired();
            builder
                .Property(u => u.LastName)
                .HasColumnType("NVARCHAR(50)")
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
                .OwnsMany(
                    user => user.RefreshTokens,
                    refreshToken =>
                    {
                        refreshToken.Property(rt => rt.Token).IsRequired();
                        refreshToken.Property(rt => rt.Expires).IsRequired();
                        refreshToken.Property(rt => rt.Created).IsRequired();
                        refreshToken.Property(rt => rt.Revoked);
                    });
            

            builder // one-to-many  Users - Loans
                .HasMany(u => u.Loans)
                .WithOne(l => l.User)
                .HasPrincipalKey(l => l.Id);

            builder // one-to-many  Users - Reviews
                .HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasPrincipalKey(r => r.Id);
        }
    }
}
