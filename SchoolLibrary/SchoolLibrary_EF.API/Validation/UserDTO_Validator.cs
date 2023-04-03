using FluentValidation;
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
                .WithMessage("A first name length of more than 50 characters is not allowed");

            RuleFor(entity => entity.LastName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("A last name length of more than 50 characters is not allowed");

            RuleFor(entity => entity.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithMessage("The email entered must meet the email standard");
        }
    }
}