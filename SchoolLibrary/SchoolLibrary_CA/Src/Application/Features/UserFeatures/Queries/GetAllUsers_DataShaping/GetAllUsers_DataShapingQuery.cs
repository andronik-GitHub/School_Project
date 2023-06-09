﻿using System.Dynamic;
using Application.Common.HATEOS;
using Application.Common.Pagging;
using Application.Common.Pagging.Entities;
using MediatR;

namespace Application.Features.UserFeatures.Queries.GetAllUsers_DataShaping
{
    public class GetAllUsers_DataShapingQuery : IRequest<PagedList<ShapedEntity>>
    {
        public UserParameter _parameters { get; }

        public GetAllUsers_DataShapingQuery(UserParameter parameters)
        {
            _parameters = parameters;
        }
    }
}