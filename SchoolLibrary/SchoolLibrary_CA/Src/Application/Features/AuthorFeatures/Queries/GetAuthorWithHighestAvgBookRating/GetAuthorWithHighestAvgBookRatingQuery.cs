using Application.Features.AuthorFeatures.Queries.Common;
using MediatR;

namespace Application.Features.AuthorFeatures.Queries.GetAuthorWithHighestAvgBookRating
{
    /// <summary>
    /// Gets author with highest avg book rating
    /// </summary>
    public class GetAuthorWithHighestAvgBookRatingQuery : IRequest<GetDTO_AuthorWithHighestAvgBookRating>
    {
        /// <summary>
        /// Gets author with highest avg book rating
        /// </summary>
        /// <returns>Returns author with highest avg book rating</returns>
        public GetAuthorWithHighestAvgBookRatingQuery()
        {
        }
    }
}