using Application.Common.Interfaces;
using Application.Common.Mapping.Mapster;
using Application.Features.AuthorFeatures.Queries.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.AuthorFeatures.Queries.GetAllAuthors
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, IEnumerable<AuthorDTO>>
    {
        private readonly ISchoolLibraryContext _context;
        private readonly ISortHelper<Author> _sortHelper;

        public GetAllAuthorsQueryHandler(ISchoolLibraryContext context, ISortHelper<Author> sortHelper)
        {
            _context = context;
            _sortHelper = sortHelper;
        }


        public async Task<IEnumerable<AuthorDTO>> Handle(GetAllAuthorsQuery query, CancellationToken cancellationToken)
        {
            // Filtering
            var list = _context.Authors.AsNoTracking().Where(a => 
                a.Birthdate.Year >= query._parameters.MinYearOfBirth && 
                a.Birthdate.Year <= query._parameters.MaxYearOfBirth);
            
            // Searching
            SearchByName(ref list, query._parameters.FullName);

            // Sorting
            list = _sortHelper.ApplySort(list, query._parameters.OrderBy);
            
            // Paging
            return await MapsterFunctions.MapListSourceToDestination<Author, AuthorDTO>(
                list
                    //.OrderBy(a => a.AuthorId)
                    .Skip((query._parameters.PageNumber - 1) * query._parameters.PageSize)
                    .Take(query._parameters.PageSize))
                .ToListAsync(cancellationToken);
        }

        private static void SearchByName(ref IQueryable<Author> entities, string? Name)
        {
            if (!entities.Any() || string.IsNullOrWhiteSpace(Name)) return;

            entities = entities
                .Where(p => (p.FirstName + " " + p.LastName).ToLower().Contains(Name.Trim().ToLower()));
        }
    }
}