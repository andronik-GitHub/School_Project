using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations.DefaultConfiguration
{
    public class LoansConfiguration : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder
                .HasKey(l => l.LoanId);
            builder
                .Property(l => l.LoanId)
                .ValueGeneratedOnAdd();
            
            builder
                .Property(e => e.DateCreated)
                .HasDefaultValueSql("GETUTCDATE()");


            builder
                .Property(l => l.UserId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();
            builder
                .Property(l => l.BookId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();
            builder
                .Property(l => l.LoanDate)
                .HasColumnType("DATETIME")
                .IsRequired();
            builder
                .Property(l => l.ReturnDate)
                .HasColumnType("DATETIME")
                .IsRequired();
            

            builder // one-to-many  Loans - Users
                .HasOne(l => l.User)
                .WithMany(u => u.Loans)
                .HasForeignKey(l => l.UserId);

            builder // one-to-many  Loans - Book
                .HasOne(l => l.Book)
                .WithMany(b => b.Loans)
                .HasForeignKey(l => l.BookId);
        }
    }
}
