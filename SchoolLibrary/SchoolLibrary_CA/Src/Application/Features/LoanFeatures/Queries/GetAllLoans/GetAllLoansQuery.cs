﻿using Application.Common.Pagging.Entities;
using Application.Features.LoanFeatures.Queries.Common;
using MediatR;

namespace Application.Features.LoanFeatures.Queries.GetAllLoans
{
    public class GetAllLoansQuery : IRequest<IEnumerable<LoanDTO>>
    {
        public LoanParameter _parameters { get; }

        public GetAllLoansQuery(LoanParameter parameters)
        {
            _parameters = parameters;
        }
    }
}