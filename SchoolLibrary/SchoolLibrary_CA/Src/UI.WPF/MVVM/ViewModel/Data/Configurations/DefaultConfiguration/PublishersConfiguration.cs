﻿using UI.WPF.MVVM.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UI.WPF.MVVM.ViewModel.Data.Configurations.DefaultConfiguration
{
    public class PublishersConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder
                .HasKey(p => p.PublisherId);
            builder
                .Property(p => p.PublisherId)
                .ValueGeneratedOnAdd();
            
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
