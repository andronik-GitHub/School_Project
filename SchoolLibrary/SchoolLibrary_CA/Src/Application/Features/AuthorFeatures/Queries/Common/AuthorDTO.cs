﻿using Application.Common.HATEOS;

namespace Application.Features.AuthorFeatures.Queries.Common
{
    public class AuthorDTO : LinkBaseEntity
    {
        public Guid AuthorId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime Birthdate { get; set; }
        public string Nationality { get; set; } = default!;
    }
}