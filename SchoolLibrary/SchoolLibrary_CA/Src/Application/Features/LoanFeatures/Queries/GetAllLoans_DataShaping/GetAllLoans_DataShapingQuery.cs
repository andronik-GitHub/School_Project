using System.Dynamic;
using Application.Common.HATEOS;
using Application.Common.Pagging;
using Application.Common.Pagging.Entities;
using MediatR;

namespace Application.Features.LoanFeatures.Queries.GetAllLoans_DataShaping
{
    public class GetAllLoans_DataShapingQuery : IRequest<PagedList<ShapedEntity>>
    {
        public LoanParameter _parameters { get; }

        public GetAllLoans_DataShapingQuery(LoanParameter parameters)
        {
            _parameters = parameters;
        }
    }
}