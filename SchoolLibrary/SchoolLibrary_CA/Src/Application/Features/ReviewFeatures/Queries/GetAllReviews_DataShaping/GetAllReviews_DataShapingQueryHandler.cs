using System.Dynamic;
using Application.Common.HATEOS;
using Application.Common.Interfaces;
using Application.Common.Pagging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ReviewFeatures.Queries.GetAllReviews_DataShaping
{
    public class GetAllReviews_DataShapingQueryHandler
        : IRequestHandler<GetAllReviews_DataShapingQuery, PagedList<ShapedEntity>>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly ISortHelper<Review> _sortHelper;
        private readonly IDataShaper<Review> _dataShaper;
        
        public GetAllReviews_DataShapingQueryHandler(
            ISchoolLibraryContext context,
            ISortHelper<Review> sortHelper,
            IDataShaper<Review> dataShaper)
        {
            _context = context;
            _sortHelper = sortHelper;
            _dataShaper = dataShaper;
        }


        public async Task<PagedList<ShapedEntity>> Handle
            (GetAllReviews_DataShapingQuery query, CancellationToken cancellationToken)
        {
            // Filtering
            var list = _context.Reviews
                .AsNoTracking()
                .Include(r => r.User)
                .Include(r => r.Book)
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