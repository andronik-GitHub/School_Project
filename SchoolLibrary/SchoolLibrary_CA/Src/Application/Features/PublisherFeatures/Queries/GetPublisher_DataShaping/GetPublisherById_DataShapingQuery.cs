﻿using System.Dynamic;
using Application.Common.HATEOS;
using Application.Common.Pagging.Entities;
using MediatR;

namespace Application.Features.PublisherFeatures.Queries.GetPublisher_DataShaping
{
    public class GetPublisherById_DataShapingQuery : IRequest<ShapedEntity>
    {
        public Guid Id { get; set; } = default!;
        public PublisherParameter _parameters { get; }

        public GetPublisherById_DataShapingQuery(PublisherParameter parameters)
        {
            _parameters = parameters;
        }
    }
}