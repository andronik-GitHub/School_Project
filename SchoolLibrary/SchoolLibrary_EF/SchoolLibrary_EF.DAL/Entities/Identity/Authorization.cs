﻿namespace SchoolLibrary_EF.DAL.Entities.Identity
{
    public static class Authorization
    {
        public enum Roles
        {
            Administrator,
            Moderator,
            User
        }

        public const string default_username = "user_";
        public const string default_password = "Pa##w0rd";
        public const Roles default_role = Roles.User;
    }
}