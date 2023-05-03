using System.Dynamic;
using Application.Common.HATEOS;
using Application.Common.Interfaces;
using Application.Common.Pagging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookDetailsFeatures.Queries.GetAllBookDetails_DataShaping
{
    public class GetAllBookDetails_DataShapingQueryHandler 
        : IRequestHandler<GetAllBookDetails_DataShapingQuery, PagedList<ShapedEntity>>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly ISortHelper<BookDetails> _sortHelper;
        private readonly IDataShaper<BookDetails> _dataShaper;
        
        public GetAllBookDetails_DataShapingQueryHandler(
            ISchoolLibraryContext context,
            ISortHelper<BookDetails> sortHelper,
            IDataShaper<BookDetails> dataShaper)
        {
            _context = context;
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }
        
        
        public async Task<PagedList<ShapedEntity>> Handle
            (GetAllBookDetails_DataShapingQuery query, CancellationToken cancellationToken)
        {
            // Filtering
            var list = _context.BookDetails
                .AsNoTracking()
                .Include(bd => bd.Book)
                .AsQueryable();
            
            // Sorting
            list = _sortHelper.ApplySort(list, query._parameters.OrderBy);

            // DataShaping
            var shapedList = _dataShaper.ShapeData(list, query._parameters.Fields).AsQueryable();
            
            // Paging
            return await Task.Run(
                () => PagedList<ShapedEntity>.ToPagedList(
                    shapedList, 
                    query._parameters.PageNumber, 
                    query._parameters.PageSize),
                cancellationToken);
        }
    }
}