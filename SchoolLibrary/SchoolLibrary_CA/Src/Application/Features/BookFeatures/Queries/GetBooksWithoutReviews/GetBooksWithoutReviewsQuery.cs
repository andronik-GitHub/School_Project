using Application.Common.Pagging.Entities;
using Application.Features.BookFeatures.Queries.Common;
using MediatR;

namespace Application.Features.BookFeatures.Queries.GetBooksWithoutReviews
{
    /// <summary>
    /// Gets books without reviews
    /// </summary>
    public class GetBooksWithoutReviewsQuery : IRequest<IEnumerable<GetDTO_BookWithoutReviews>>
    {
        public BookParameter _parameters { get; }

        /// <summary>
        /// Gets books without reviews
        /// </summary>
        /// <param name="parameters">BookParameters for paging</param>
        /// <returns>Returns collection of book without reviews</returns>
        public GetBooksWithoutReviewsQuery(BookParameter parameters)
        {
            _parameters = parameters;
        }
    }
}