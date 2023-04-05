using FluentValidation;
using SchoolLibrary_EF.BLL.DTO;
using SchoolLibrary_EF.BLL.DTO.Identity;

namespace SchoolLibrary_EF.API.Validation
{
    public class RegisterModel_Validator : AbstractValidator<RegisterModel>
    {
        public RegisterModel_Validator()
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
            
            RuleFor(entity => entity.UserName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("The username must not be empty, and must not exceed 50 characters in length");
            
            RuleFor(entity => entity.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithMessage("The email entered must meet the email standard");
            
            RuleFor(entity => entity.Password)
                .NotNull()
                .NotEmpty()
                .MaximumLength(25)
                .WithMessage("The password must not be empty, and must not exceed 25 characters in length");
        }
    }
}