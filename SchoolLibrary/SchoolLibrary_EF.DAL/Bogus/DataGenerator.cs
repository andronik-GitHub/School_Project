using Bogus;
using SchoolLibrary_EF.DAL.Entities;

namespace SchoolLibrary_EF.DAL.Bogus
{
    public class DataGenerator
    {
        public List<Book> Books = new();
        public List<BookDetails> BookDetails = new();
        public List<Author> Authors = new();
        public List<Publisher> Publishers = new();
        public List<User> Users = new();
        public List<Loan> Loans = new();
        public List<Review> Reviews = new();
        public List<Genre> Genres = new();
        public List<BookGenres> BookGenres = new();
        public List<BookAuthors> BookAuthors = new();

        private const int BOOKS = 0;
        private const int BOOKDETAILS = 0;
        private const int AUTHORS = 0;
        private const int PUBLISHERS = 0;
        private const int USERS = 0;
        private const int LOANS = 0;
        private const int REVIEWS = 0;
        private const int GENRES = 0;
        private const int BOOK_GENRES = 0;
        private const int BOOK_AUTHORS = 0;


        private static Faker<Book> GetBookGenerator(Guid PublisherId)
        {
            return new Faker<Book>()
                .RuleFor(b => b.BookId, _ => Guid.NewGuid())
                .RuleFor(b => b.Title, f => f.Company.CompanyName())
                .RuleFor(b => b.PublishingYear, f => f.Random.Int(1500, DateTime.Now.Year))
                .RuleFor(b => b.ISBN, _ => Guid.NewGuid().ToString().Substring(0, 13))
                .RuleFor(b => b.PublisherId, _ => PublisherId);
        }
        private static Faker<BookDetails> GetBookDetailsGenerator(Guid BookId)
        {
            return new Faker<BookDetails>()
                .RuleFor(bd => bd.BookDetailId, _ => Guid.NewGuid())
                .RuleFor(bd => bd.BookId, _ => BookId)
                .RuleFor(bd => bd.Pages, f => f.Random.Int(10, 10000))
                .RuleFor(bd => bd.Language, f => f.Lorem.Word())
                .RuleFor(bd => bd.Format, f => f.Lorem.Word());
        }
        private static Faker<Author> GetAuthorGenerator()
        {
            return new Faker<Author>()
                .RuleFor(a => a.AuthorId, _ => Guid.NewGuid())
                .RuleFor(a => a.FirstName, f => f.Name.FirstName())
                .RuleFor(a => a.LastName, f => f.Name.LastName())
                .RuleFor(a => a.Birthdate, f => f.Date.Between(DateTime.Now.AddYears(-100), DateTime.Now.AddYears(-15)))
                .RuleFor(a => a.Nationality, f => f.Lorem.Word());
        }
        private static Faker<Publisher> GetPublisherGenerator()
        {
            return new Faker<Publisher>()
                .RuleFor(p => p.PublisherId, _ => Guid.NewGuid())
                .RuleFor(p => p.Name, f => f.Company.CompanyName())
                .RuleFor(p => p.Location, f => f.Address.FullAddress());
        }
        private static Faker<User> GetUserGenerator()
        {
            return new Faker<User>()
                .RuleFor(u => u.UserId, _ => Guid.NewGuid())
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                .RuleFor(u => u.Password, f => f.Internet.Password())
                .RuleFor(u => u.Address, f => f.Address.StreetAddress())
                .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber());
        }
        private static Faker<Loan> GetLoanGenerator(Guid UserId, Guid BookId)
        {
            return new Faker<Loan>()
                .RuleFor(l => l.LoanId, _ => Guid.NewGuid())
                .RuleFor(l => l.UserId, _ => UserId)
                .RuleFor(l => l.BookId, _ => BookId)
                .RuleFor(l => l.LoanDate, f => f.Date.Between(DateTime.Now.AddYears(-10), DateTime.Now))
                .RuleFor(l => l.ReturnDate, f => f.Date.Between(DateTime.Now, DateTime.Now.AddYears(5)));
        }
    }
}
