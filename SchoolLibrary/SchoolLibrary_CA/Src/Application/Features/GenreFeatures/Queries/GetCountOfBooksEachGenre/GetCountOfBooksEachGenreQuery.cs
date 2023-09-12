using Application.Common.Pagging.Entities;
using Application.Features.GenreFeatures.Queries.Common;
using MediatR;

namespace Application.Features.GenreFeatures.Queries.GetCountOfBooksEachGenre
{
    /// <summary>
    /// Get count the number of books of each genre
    /// </summary>
    public class GetCountOfBooksEachGenreQuery : IRequest<IEnumerable<GetDTO_CountOfBooksEachGenre>>
    {
        public GenreParameter _parameters { get; }

        /// <summary>
        /// Get count the number of books of each genre
        /// </summary>
        /// <param name="parameters">GenreParameters for paging</param>
        /// <returns>Returns collection of genre names with number of books</returns>
        public GetCountOfBooksEachGenreQuery(GenreParameter parameters)
        {
            _parameters = parameters;
        }
    }
}