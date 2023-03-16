using Bogus;
using SchoolLibrary_EF.DAL.Entities;

namespace SchoolLibrary_EF.DAL.Bogus
{
    public class DataGenerator
    {
        public static List<Book> Books = new();
        public static List<BookDetails> BookDetails = new();
        public static List<Author> Authors = new();
        public static List<Publisher> Publishers = new();
        public static List<User> Users = new();
        public static List<Loan> Loans = new();
        public static List<Review> Reviews = new();
        public static List<Genre> Genres = new();
        public static List<BookGenres> BookGenres = new();
        public static List<BookAuthors> BookAuthors = new();

        private const int BOOKS = 1000;
        private const int BOOKDETAILS = BOOKS;
        private const int AUTHORS = 30;
        private const int PUBLISHERS = 50;
        private const int USERS = 300;
        private const int LOANS = BOOKS * 5;
        private const int REVIEWS = BOOKS * 10;
        private const int GENRES = 20;
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
        private static Faker<Review> GetReviewGenerator(Guid UserId)
        {
            return new Faker<Review>()
                .RuleFor(r => r.ReviewId, _ => Guid.NewGuid())
                .RuleFor(r => r.UserId, _ => UserId)
                .RuleFor(r => r.Rating, f => f.Random.Decimal(0.0m, 5.0m))
                .RuleFor(r => r.ReviewText, f => f.Lorem.Text());
        }
        private static Faker<Genre> GetGenreGenerator()
        {
            return new Faker<Genre>()
                .RuleFor(g => g.GenreId, _ => Guid.NewGuid())
                .RuleFor(g => g.Name, f => f.Company.CompanyName());
        }
        private static Faker<BookGenres> GetBookGenresGenerator(Guid BookId, Guid GenreId)
        {
            return new Faker<BookGenres>()
                .RuleFor(bg => bg.BookId, _ => BookId)
                .RuleFor(bg => bg.GenreId, _ => GenreId);
        }
        private static Faker<BookAuthors> GetBookAuthorsGenerator(Guid BookId, Guid AuthorId)
        {
            return new Faker<BookAuthors>()
                .RuleFor(ba => ba.BookId, _ => BookId)
                .RuleFor(ba => ba.AuthorId, _ => AuthorId);
        }

        private static List<Book> GetBogusBookData(Guid PublisherId)
        {
            Faker<Book> bookGenerator = GetBookGenerator(PublisherId);
            List<Book> generatorBook = bookGenerator.Generate(1);

            return generatorBook;
        }
        private static List<BookDetails> GetBogusBookDetailsData(Guid BookId)
        {
            Faker<BookDetails> bookDetailsGenerator = GetBookDetailsGenerator(BookId);
            List<BookDetails> generatorBookDetails = bookDetailsGenerator.Generate(1);

            return generatorBookDetails;
        }
        private static List<Loan> GetBogusLoanData(Guid UserId, Guid BookId)
        {
            Faker<Loan> loanGenerator = GetLoanGenerator(UserId, BookId);
            List<Loan> generatorLoan = loanGenerator.Generate(1);

            return generatorLoan;
        }
        private static List<Review> GetBogusReviewData(Guid UserId)
        {
            Faker<Review> reviewGenerator = GetReviewGenerator(UserId);
            List<Review> generatorReview = reviewGenerator.Generate(1);

            return generatorReview;
        }
        private static List<BookGenres> GetBogusBookGenresData(Guid BookId, Guid GenreId)
        {
            Faker<BookGenres> bookGenresGenerator = GetBookGenresGenerator(BookId, GenreId);
            List<BookGenres> generatorBookGenres = bookGenresGenerator.Generate(1);

            return generatorBookGenres;
        }
        private static List<BookAuthors> GetBookAuthorsData(Guid BookId, Guid AuthorId)
        {
            Faker<BookAuthors> bookAuthorsGenerator = GetBookAuthorsGenerator(BookId, AuthorId);
            List<BookAuthors> generatorBookAuthors = bookAuthorsGenerator.Generate(1);

            return generatorBookAuthors;
        }

        public static void InitBogusData()
        {
            Authors.AddRange(GetAuthorGenerator().Generate(AUTHORS));
            Publishers.AddRange(GetPublisherGenerator().Generate(PUBLISHERS));
            Users.AddRange(GetUserGenerator().Generate(USERS));
            Genres.AddRange(GetGenreGenerator().Generate(GENRES));

            Publishers.ForEach(publisher => Books.AddRange(GetBogusBookData(publisher.PublisherId)));
            Books.ForEach(book => BookDetails.AddRange(GetBogusBookDetailsData(book.BookId)));

            for (int i = 0; i < LOANS; i++)
                Loans.AddRange(GetBogusLoanData(
                        Users[new Random().Next(0, USERS - 1)].UserId,
                        Books[new Random().Next(0, BOOKS - 1)].BookId
                    ));
        }
    }
}
