﻿using Application.Common.HATEOS;

namespace Application.Features.ReviewFeatures.Queries.Common
{
    public class ReviewDTO: LinkBaseEntity
    {
        public Guid ReviewId { get; set; }
        public decimal Rating { get; set; }
        public string ReviewText { get; set; } = default!;
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
    }
}