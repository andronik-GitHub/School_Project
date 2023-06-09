﻿using SchoolLibrary_EF.BLL.DTO.HATEOAS;

namespace SchoolLibrary_EF.BLL.DTO
{
    public class ReviewDTO : LinkBaseEntity
    {
        public Guid ReviewId { get; set; }
        public decimal Rating { get; set; }
        public string? ReviewText { get; set; } = default!;
        //public Guid UserId { get; set; }
        //public Guid BookId { get; set; }
        public string? UserFullName { get; set; } = default!;
        public string? BookTitle { get; set; } = default!;
    }
}
