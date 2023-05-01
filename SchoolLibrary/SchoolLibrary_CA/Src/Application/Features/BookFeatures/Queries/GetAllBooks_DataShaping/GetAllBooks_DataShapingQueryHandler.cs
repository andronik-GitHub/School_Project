using System.Dynamic;
using Application.Common.Interfaces;
using Application.Common.Pagging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookFeatures.Queries.GetAllBooks_DataShaping
{
    public class GetAllBooks_DataShapingQueryHandler 
        : IRequestHandler<GetAllBooks_DataShapingQuery, IEnumerable<ExpandoObject>>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly ISortHelper<Book> _sortHelper;
        private readonly IDataShaper<Book> _dataShaper;
        
        public GetAllBooks_DataShapingQueryHandler(
            ISchoolLibraryContext context,
            ISortHelper<Book> sortHelper,
            IDataShaper<Book> dataShaper)
        {
            _context = context;
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }


        public async Task<IEnumerable<ExpandoObject>> Handle
            (GetAllBooks_DataShapingQuery query, CancellationToken cancellationToken)
        {
            // Filtering
            var list = _context.Books.AsNoTracking().Include(b => b.Publisher).AsQueryable();
            
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