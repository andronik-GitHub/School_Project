﻿namespace SchoolLibrary_EF.BLL.DTOs.BookDTOs
{
    public class UpdateDTO_Book
    {
        public Guid BookId { get; set; }
        public string Title { get; set; } = default!;
        public int PublishingYear { get; set; }
        public string ISBN { get; set; } = default!;
        public Guid PublisherId { get; set; }
    }
}