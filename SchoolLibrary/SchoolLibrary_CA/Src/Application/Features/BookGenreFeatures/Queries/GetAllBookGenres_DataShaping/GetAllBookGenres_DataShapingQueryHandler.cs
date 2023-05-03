using System.Dynamic;
using Application.Common.HATEOS;
using Application.Common.Interfaces;
using Application.Common.Pagging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookGenreFeatures.Queries.GetAllBookGenres_DataShaping
{
    public class GetAllBookGenres_DataShapingQueryHandler 
        : IRequestHandler<GetAllBookGenres_DataShapingQuery, PagedList<ShapedEntity>>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly ISortHelper<BookGenres> _sortHelper;
        private readonly IDataShaper<BookGenres> _dataShaper;

        public GetAllBookGenres_DataShapingQueryHandler(
            ISchoolLibraryContext context,
            ISortHelper<BookGenres> sortHelper,
            IDataShaper<BookGenres> dataShaper)
        {
            _context = context;
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }

        public async Task<PagedList<ShapedEntity>> Handle
            (GetAllBookGenres_DataShapingQuery query, CancellationToken cancellationToken)
        {
            // Filtering
            var list = _context.BookGenres
                .AsNoTracking()
                .Include(bg => bg.Book)
                .Include(bg => bg.Genre)
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