using System.Dynamic;
using Application.Common.Pagging;
using Application.Common.Pagging.Entities;
using MediatR;

namespace Application.Features.ReviewFeatures.Queries.GetAllReviews_DataShaping
{
    public class GetAllReviews_DataShapingQuery : IRequest<PagedList<ExpandoObject>>
    {
        public ReviewParameter _parameters { get; }

        public GetAllReviews_DataShapingQuery(ReviewParameter parameters)
        {
            _parameters = parameters;
        }
    }
}