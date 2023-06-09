﻿using System.Dynamic;
using Application.Common.HATEOS;
using Application.Common.Pagging;
using Application.Common.Pagging.Entities;
using MediatR;

namespace Application.Features.BookDetailsFeatures.Queries.GetAllBookDetails_DataShaping
{
    public class GetAllBookDetails_DataShapingQuery : IRequest<PagedList<ShapedEntity>>
    {
        public BookDetailsParameter _parameters { get; }

        public GetAllBookDetails_DataShapingQuery(BookDetailsParameter parameters)
        {
            _parameters = parameters;
        }
    }
}