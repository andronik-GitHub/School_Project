using SchoolLibrary_Dapper.DAL.Entities;
using SchoolLibrary_Dapper.DAL.Repositories.Contracts;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace SchoolLibrary_Dapper.DAL.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(
            SqlConnection sqlConnection,
            IDbTransaction dbTransaction)
            : base(sqlConnection, dbTransaction, "Books")
        {
        }

        public async Task<(Book, Author, Genre)?> GetByIdBookWithAuthorAndGenreAsync(Guid id)
        {
            var query = "SELECT b.BookId AS BookId, b.Title AS Title, b.ISBN AS ISBN, " +
                        " a.AuthorId AS AuthorId, a.FirstName AS FirstName, a.LastName AS LastName, " +
                        " g.GenreId AS GenreId, g.Name AS Name " +
                        $"FROM [{tableName}] b " +
                        "INNER JOIN BookAuthors ba ON b.BookId = ba.BookId " +
                        "INNER JOIN Authors a ON ba.AuthorId = a.AuthorId " +
                        "INNER JOIN BookGenres bg ON b.BookId = bg.BookId " +
                        "INNER JOIN Genres g ON bg.GenreId = g.GenreId " +
                        "WHERE b.BookId = @Id";
    
            var result = await sqlConnection.QueryAsync<Book, Author, Genre, (Book, Author, Genre)>(
                query,
                (book, author, genre) => (book, author, genre),
                param: new { Id = id },
                transaction: dbTransaction,
                splitOn: "BookId, AuthorId, GenreId");

            return result.SingleOrDefault();
        }
        public async Task<IEnumerable<(Book, Author, Genre)>> GetBooksWithAuthorsAndGenresAsync()
        {
            var query = "SELECT b.BookId AS BookId, b.Title AS Title, b.ISBN AS ISBN, " +
                        " a.AuthorId AS AuthorId, a.FirstName AS FirstName, a.LastName AS LastName, " +
                        " g.GenreId AS GenreId, g.Name AS \"Name\" " +
                        $"FROM [{tableName}] b " +
                        "INNER JOIN BookAuthors ba ON b.BookId = ba.BookId " +
                        "INNER JOIN Authors a ON ba.AuthorId = a.AuthorId " +
                        "INNER JOIN BookGenres bg ON b.BookId = bg.BookId " +
                        "INNER JOIN Genres g ON bg.GenreId = g.GenreId ";
            
            var result = await sqlConnection.QueryAsync<Book, Author, Genre, (Book, Author, Genre)>(
                query,
                (book, author, genre) => (book, author, genre),
                splitOn: "BookId, AuthorId, GenreId",
                transaction: dbTransaction);

            return result.AsEnumerable();
        }
    }
}
