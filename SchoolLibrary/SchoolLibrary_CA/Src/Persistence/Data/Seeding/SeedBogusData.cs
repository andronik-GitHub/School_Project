using Application.System.Commands.SeedBogusData;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data.Seeding
{
    public static class SeedBogusData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(DataGenerator.Books);
            modelBuilder.Entity<BookDetails>().HasData(DataGenerator.BookDetails);
            modelBuilder.Entity<Author>().HasData(DataGenerator.Authors);
            modelBuilder.Entity<Publisher>().HasData(DataGenerator.Publishers);
            modelBuilder.Entity<User>(entity => // seed entity with ValueObject
            {
                DataGenerator.Users.ForEach(user =>
                {
                    entity.HasData(new 
                    {
                        user.UserId,
                        user.Email,
                        user.Password,
                        user.Phone
                    });
                    
                    entity.OwnsOne(p => p.Address, address =>
                        {
                            address.OwnsOne(a => a.Street).HasData(new
                            {
                                AddressUserId = user.UserId,
                                user.Address.Street.Value
                            });
                            address.OwnsOne(a => a.City).HasData(new
                            {
                                AddressUserId = user.UserId,
                                user.Address.City.Value
                            });
                            address.OwnsOne(a => a.Country).HasData(new
                            {
                                AddressUserId = user.UserId,
                                user.Address.Country.Value
                            });
                        });
                    entity.OwnsOne(p => p.UserName, userName =>
                        {
                            userName.OwnsOne(a => a.FirstName).HasData(new
                            {
                                UserNameUserId = user.UserId,
                                user.UserName.FirstName.Value
                            });
                            userName.OwnsOne(a => a.LastName).HasData(new
                            {
                                UserNameUserId = user.UserId,
                                user.UserName.LastName.Value
                            });
                        });
                });
            });
            modelBuilder.Entity<Loan>().HasData(DataGenerator.Loans);
            modelBuilder.Entity<Review>().HasData(DataGenerator.Reviews);
            modelBuilder.Entity<Genre>().HasData(DataGenerator.Genres);
            modelBuilder.Entity<BookGenres>().HasData(DataGenerator.BookGenres);
            modelBuilder.Entity<BookAuthors>().HasData(DataGenerator.BookAuthors);
        }
    }
}