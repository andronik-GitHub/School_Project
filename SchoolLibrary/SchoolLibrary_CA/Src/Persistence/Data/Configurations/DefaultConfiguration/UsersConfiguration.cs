using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations.DefaultConfiguration
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            #region Configuration ValueObjects
            {
                builder.OwnsOne(u => u.UserName, userName =>
                {
                    userName.OwnsOne(un => un.FirstName)
                        .Property(x => x.Value)
                        .HasColumnName("FirstName")
                        .HasColumnType("NVARCHAR(50)")
                        .IsRequired();
                
                    userName.OwnsOne(un => un.LastName)
                        .Property(x => x.Value)
                        .HasColumnName("LastName")
                        .HasColumnType("NVARCHAR(50)")
                        .IsRequired();
                });
                
                builder.OwnsOne(u => u.Address, address =>
                {
                    address.OwnsOne(ua => ua.Country)
                        .Property(x => x.Value)
                        .HasColumnName("Country")
                        .HasColumnType("NVARCHAR(50)")
                        .IsRequired();
                    
                    address.OwnsOne(ua => ua.City)
                        .Property(x => x.Value)
                        .HasColumnName("City")
                        .HasColumnType("NVARCHAR(50)")
                        .IsRequired();

                    address.OwnsOne(ua => ua.Street)
                        .Property(x => x.Value)
                        .HasColumnName("Street")
                        .HasColumnType("NVARCHAR(80)")
                        .IsRequired();
                });
            }
            #endregion
            
            
            builder
                .HasKey(u => u.UserId);
            builder
                .Property(u => u.UserId)
                .ValueGeneratedOnAdd();

            builder
                .Property(e => e.DateCreated)
                .HasDefaultValueSql("GETUTCDATE()");

            
            builder
                .Property(p => p.Email)
                .HasColumnType("NVARCHAR(80)")
                .IsRequired();
            builder
                .Property(p => p.Password)
                .HasColumnType("NVARCHAR(30)")
                .IsRequired();
            builder
                .Property(p => p.Phone)
                .HasColumnType("NVARCHAR(20)")
                .IsRequired();
            
            
            // Relationships
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
