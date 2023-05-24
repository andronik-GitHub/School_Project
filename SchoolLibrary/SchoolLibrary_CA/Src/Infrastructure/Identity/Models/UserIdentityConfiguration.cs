using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Identity.Models
{
    public class UserIdentityConfiguration : IEntityTypeConfiguration<UserIdentity>
    {
        public void Configure(EntityTypeBuilder<UserIdentity> builder)
        {
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
                .OwnsMany(
                    user => user.RefreshTokens,
                    refreshToken =>
                    {
                        // Created table in the Creating Shared Database.Console application has PK { User Id , Id }
                        // Without specifying the name of the FK, there will be an error of names (UserIdentityId)
                        refreshToken.WithOwner().HasForeignKey("UserId");
                        
                        refreshToken.Property(rt => rt.Token).IsRequired();
                        refreshToken.Property(rt => rt.Expires).IsRequired();
                        refreshToken.Property(rt => rt.Created).IsRequired();
                        refreshToken.Property(rt => rt.Revoked);
                    });
        }
    }
}