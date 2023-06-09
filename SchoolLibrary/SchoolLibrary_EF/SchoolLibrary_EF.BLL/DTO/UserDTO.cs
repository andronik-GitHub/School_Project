﻿using SchoolLibrary_EF.BLL.DTO.HATEOAS;

namespace SchoolLibrary_EF.BLL.DTO
{
    public class UserDTO : LinkBaseEntity
    {
        public Guid UserId { get; set; }
        public string? FirstName { get; set; } = default!;
        public string? LastName { get; set; } = default!;
        public string? Email { get; set; } = default!;
        //public string Password { get; set; } = default!;
        //public string Address { get; set; } = default!;
        //public string Phone { get; set; } = default!;
    }
}
