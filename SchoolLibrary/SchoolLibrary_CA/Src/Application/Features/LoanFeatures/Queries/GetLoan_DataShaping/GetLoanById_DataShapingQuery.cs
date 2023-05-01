using System.Dynamic;
using Application.Common.Pagging.Entities;
using MediatR;

namespace Application.Features.LoanFeatures.Queries.GetLoan_DataShaping
{
    public class GetLoanById_DataShapingQuery : IRequest<ExpandoObject>
    {
        public Guid Id { get; set; } = default!;
        public LoanParameter _parameters { get; }

        public GetLoanById_DataShapingQuery(LoanParameter parameters)
        {
            _parameters = parameters;
        }
    }
}