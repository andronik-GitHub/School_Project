﻿using FluentValidation;
using SchoolLibrary_EF.BLL.DTO;

namespace SchoolLibrary_EF.API.Validation
{
    public class UserDTO_Validator : AbstractValidator<UserDTO>
    {
        public UserDTO_Validator()
        {
            RuleFor(entity => entity.FirstName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("The first name must not be empty, and must not exceed 50 characters in length");

            RuleFor(entity => entity.LastName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("The last name must not be empty, and must not exceed 50 characters in length");

            RuleFor(entity => entity.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithMessage("The email entered must meet the email standard");
        }
    }
}