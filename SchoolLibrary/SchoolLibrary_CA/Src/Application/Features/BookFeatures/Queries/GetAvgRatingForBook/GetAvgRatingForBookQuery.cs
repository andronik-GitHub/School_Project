using Application.Common.Pagging.Entities;
using Application.Features.BookFeatures.Queries.Common;
using MediatR;

namespace Application.Features.BookFeatures.Queries.GetAvgRatingForBook
{
    /// <summary>
    /// Get average rating for each book
    /// </summary>
    public class GetAvgRatingForBookQuery : IRequest<IEnumerable<GetDTO_AvgRatingBook>>
    {
        public BookParameter _parameters { get; }

        /// <summary>
        /// Get average rating for each book
        /// </summary>
        /// <param name="parameters">BookParameters for paging</param>
        /// <returns>Returns collection of book titles with average rating</returns>
        public GetAvgRatingForBookQuery(BookParameter parameters)
        {
            _parameters = parameters;
        }
    }
}