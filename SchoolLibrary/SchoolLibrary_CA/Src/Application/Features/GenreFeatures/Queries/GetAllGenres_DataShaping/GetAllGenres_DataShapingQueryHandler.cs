using System.Dynamic;
using Application.Common.HATEOS;
using Application.Common.Interfaces;
using Application.Common.Pagging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.GenreFeatures.Queries.GetAllGenres_DataShaping
{
    public class GetAllGenres_DataShapingQueryHandler 
        : IRequestHandler<GetAllGenres_DataShapingQuery, PagedList<ShapedEntity>>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly ISortHelper<Genre> _sortHelper;
        private readonly IDataShaper<Genre> _dataShaper;
        
        public GetAllGenres_DataShapingQueryHandler(
            ISchoolLibraryContext context,
            ISortHelper<Genre> sortHelper,
            IDataShaper<Genre> dataShaper)
        {
            _context = context;
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }


        public async Task<PagedList<ShapedEntity>> Handle
            (GetAllGenres_DataShapingQuery query, CancellationToken cancellationToken)
        {
            // Filtering
            var list = _context.Genres.AsNoTracking();
            
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