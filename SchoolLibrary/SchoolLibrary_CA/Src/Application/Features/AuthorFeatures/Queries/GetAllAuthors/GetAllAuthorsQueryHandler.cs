﻿using Application.Common.Interfaces;
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

        public GetAllAuthorsQueryHandler(ISchoolLibraryContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<AuthorDTO>> Handle(GetAllAuthorsQuery query, CancellationToken cancellationToken)
        {
            var list = MapsterFunctions.MapListSourceToDestination<Author, AuthorDTO>(await _context.Authors
                .AsNoTracking()
                .ToListAsync(cancellationToken));

            return list.ToList().AsReadOnly();
        }
    }
}