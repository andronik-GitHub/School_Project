using System.Dynamic;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.LoanFeatures.Queries.GetLoan_DataShaping
{
    public class GetLoanById_DataShapingQueryHandler : IRequestHandler<GetLoanById_DataShapingQuery, ExpandoObject>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly IDataShaper<Loan> _dataShaper;

        public GetLoanById_DataShapingQueryHandler(
            ISchoolLibraryContext context, 
            IDataShaper<Loan> dataShaper)
        {
            _context = context;
            _dataShaper = dataShaper;
        }


        public async Task<ExpandoObject> Handle(GetLoanById_DataShapingQuery query, CancellationToken cancellationToken)
        {
            var entity = await _context.Loans
                .AsNoTracking()
                .FirstAsync(bd => bd.LoanId == query.Id, cancellationToken);
                
            return entity == null ? 
                throw new NotFoundException(nameof(Loan), query.Id) :
                _dataShaper.ShapeData(entity, query._parameters.Fields ?? "");
        }
    }
}