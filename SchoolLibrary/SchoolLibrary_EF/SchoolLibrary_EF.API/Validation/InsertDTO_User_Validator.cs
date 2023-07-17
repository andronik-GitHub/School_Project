using FluentValidation;
using SchoolLibrary_EF.BLL.DTOs.UserDTOs;

namespace SchoolLibrary_EF.API.Validation
{
    public class InsertDTO_User_Validator : AbstractValidator<InsertDTO_User>
    {
        public InsertDTO_User_Validator()
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

            RuleFor(entity => entity.Country)
                .NotNull()
                .NotEmpty()
                .MaximumLength(80)
                .WithMessage("The country must not be empty, and must not exceed 50 characters in length");

            RuleFor(entity => entity.City)
                .NotNull()
                .NotEmpty()
                .MaximumLength(80)
                .WithMessage("The city must not be empty, and must not exceed 50 characters in length");

            RuleFor(entity => entity.Street)
                .NotNull()
                .NotEmpty()
                .MaximumLength(80)
                .WithMessage("The street must not be empty, and must not exceed 80 characters in length");
        }
    }
}