using System.Dynamic;
using Application.Common.Interfaces;
using Application.Common.Pagging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.LoanFeatures.Queries.GetAllLoans_DataShaping
{
    public class GetAllLoans_DataShapingQueryHandler 
        : IRequestHandler<GetAllLoans_DataShapingQuery, PagedList<ExpandoObject>>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly ISortHelper<Loan> _sortHelper;
        private readonly IDataShaper<Loan> _dataShaper;
        
        public GetAllLoans_DataShapingQueryHandler(
            ISchoolLibraryContext context,
            ISortHelper<Loan> sortHelper,
            IDataShaper<Loan> dataShaper)
        {
            _context = context;
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }


        public async Task<PagedList<ExpandoObject>> Handle
            (GetAllLoans_DataShapingQuery query, CancellationToken cancellationToken)
        {
            // Filtering
            var list = _context.Loans
                .AsNoTracking()
                .Include(l => l.User)
                .Include(l => l.Book)
                .AsQueryable();
            
            // Sorting
            list = _sortHelper.ApplySort(list, query._parameters.OrderBy);

            // DataShaping
            var shapedList = _dataShaper.ShapeData(list, query._parameters.Fields).AsQueryable();
            
            // Paging
            return await Task.Run(
                () => PagedList<ExpandoObject>.ToPagedList(
                    shapedList, 
                    query._parameters.PageNumber, 
                    query._parameters.PageSize),
                cancellationToken);
        }
    }
}