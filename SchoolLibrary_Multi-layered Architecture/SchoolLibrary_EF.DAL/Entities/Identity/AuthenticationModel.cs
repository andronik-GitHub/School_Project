﻿using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SchoolLibrary_EF.DAL.Entities.Identity
{
    public class AuthenticationModel
    {
        public string? Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public List<string>? Roles { get; set; }
        public string? Token { get; set; }
        [JsonIgnore]
        [Required]
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}