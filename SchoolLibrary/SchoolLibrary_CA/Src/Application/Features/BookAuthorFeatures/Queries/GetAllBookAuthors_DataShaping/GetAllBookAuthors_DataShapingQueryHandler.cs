using System.Dynamic;
using Application.Common.HATEOS;
using Application.Common.Interfaces;
using Application.Common.Pagging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookAuthorFeatures.Queries.GetAllBookAuthors_DataShaping
{
    public class GetAllBookAuthors_DataShapingQueryHandler 
        : IRequestHandler<GetAllBookAuthors_DataShapingQuery, PagedList<ShapedEntity>>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly ISortHelper<BookAuthors> _sortHelper;
        private readonly IDataShaper<BookAuthors> _dataShaper;

        public GetAllBookAuthors_DataShapingQueryHandler(
            ISchoolLibraryContext context,
            ISortHelper<BookAuthors> sortHelper,
            IDataShaper<BookAuthors> dataShaper)
        {
            _context = context;
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }

        public async Task<PagedList<ShapedEntity>> Handle
            (GetAllBookAuthors_DataShapingQuery query, CancellationToken cancellationToken)
        {
            // Filtering
            var list = _context.BookAuthors
                .Include(bg => bg.Book)
                .Include(bg => bg.Author)
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