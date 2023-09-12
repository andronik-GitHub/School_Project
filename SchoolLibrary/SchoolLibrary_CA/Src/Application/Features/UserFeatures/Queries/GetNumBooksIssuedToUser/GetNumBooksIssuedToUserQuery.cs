using Application.Common.Pagging.Entities;
using Application.Features.UserFeatures.Queries.Common;
using MediatR;

namespace Application.Features.UserFeatures.Queries.GetNumBooksIssuedToUser
{
    /// <summary>
    /// Gets number of books issued per user
    /// </summary>
    public class GetNumBooksIssuedToUserQuery : IRequest<IEnumerable<GetDTO_NumBooksIssuedToUser>>
    {
        public UserParameter _parameters { get; }

        /// <summary>
        /// Gets number of books issued per user
        /// </summary>
        /// <param name="parameters">UserParameters for paging</param>
        /// <returns>Returns collection of users with count of books loaned</returns>
        public GetNumBooksIssuedToUserQuery(UserParameter parameters)
        {
            _parameters = parameters;
        }
    }
}