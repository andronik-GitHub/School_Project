using System.Dynamic;
using Application.Common.Pagging.Entities;
using MediatR;

namespace Application.Features.ReviewFeatures.Queries.GetReview_DataShaping
{
    public class GetReviewById_DataShapingQuery : IRequest<ExpandoObject>
    {
        public Guid Id { get; set; } = default!;
        public ReviewParameter _parameters { get; }

        public GetReviewById_DataShapingQuery(ReviewParameter parameters)
        {
            _parameters = parameters;
        }
    }
}