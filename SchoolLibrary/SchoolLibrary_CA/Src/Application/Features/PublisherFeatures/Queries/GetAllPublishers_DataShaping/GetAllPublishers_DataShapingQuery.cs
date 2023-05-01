﻿using System.Dynamic;
using Application.Common.Pagging;
using Application.Common.Pagging.Entities;
using MediatR;

namespace Application.Features.PublisherFeatures.Queries.GetAllPublishers_DataShaping
{
    public class GetAllPublishers_DataShapingQuery : IRequest<PagedList<ExpandoObject>>
    {
        public PublisherParameter _parameters { get; }

        public GetAllPublishers_DataShapingQuery(PublisherParameter parameters)
        {
            _parameters = parameters;
        }
    }
}