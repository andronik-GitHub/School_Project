using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations.DefaultConfiguration
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
                .Ignore(u => u.Address)
                .Ignore(u => u.UserName);


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
            

            builder // one-to-many  Users - Loans
                .HasMany(u => u.Loans)
                .WithOne(l => l.User)
                .HasPrincipalKey(l => l.UserId);

            builder // one-to-many  Users - Reviews
                .HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasPrincipalKey(r => r.UserId);


            #region Configuration ValueObjects
            {
                builder.OwnsOne(u => u.UserName, entity =>
                {
                    entity.OwnsOne(un => un.FirstName, fn =>
                    {
                        fn.Property(x => x.Value).HasColumnName("FirstName");
                        fn.Property(x => x.Value).HasColumnType("NVARCHAR(50)");
                        fn.Property(x => x.Value).IsRequired();
                    });
                
                    entity.OwnsOne(un => un.LastName, ln =>
                    {
                        ln.Property(x => x.Value).HasColumnName("LastName");
                        ln.Property(x => x.Value).HasColumnType("NVARCHAR(50)");
                        ln.Property(x => x.Value).IsRequired();
                    });
                });

                builder.OwnsOne(u => u.Address, entity =>
                {
                    entity.Property(p => p.Country).HasColumnName("Country");
                    entity.Property(p => p.Country).HasColumnType("NVARCHAR(50)");
                    entity.Property(p => p.Country).IsRequired();
                
                    entity.Property(p => p.City).HasColumnName("City");
                    entity.Property(p => p.City).HasColumnType("NVARCHAR(50)");
                    entity.Property(p => p.City).IsRequired();
                
                    entity.Property(p => p.Street).HasColumnName("Street");
                    entity.Property(p => p.Street).HasColumnType("NVARCHAR(80)");
                    entity.Property(p => p.Street).IsRequired();
                });
            }
            #endregion
        }
    }
}
