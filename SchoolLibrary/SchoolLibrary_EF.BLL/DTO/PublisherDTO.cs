﻿namespace SchoolLibrary_EF.BLL.DTO
{
    public class PublisherDTO
    {
        public Guid PublisherId { get; set; }
        public string Name { get; set; } = default!;
        public string Location { get; set; } = default!;
    }
}