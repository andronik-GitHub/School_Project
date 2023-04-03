using FluentValidation.TestHelper;
using Microsoft.IdentityModel.Tokens;
using SchoolLibrary_EF.API.Validation;
using SchoolLibrary_EF.BLL.DTO;
using Xunit;

namespace SchoolLibrary_EF.Tests.Validation
{
    public class UserDTO_ValidatorTests
    {
        private readonly UserDTO_Validator _validator = new UserDTO_Validator();

        [Theory]
        [InlineData("Lorein", "Johnson", "exapmle.email@gmail.com")]
        [InlineData("", "Johnson", "exapmle.email@gmail.com")]
        [InlineData("Lorein", "", "exapmle.email@gmail.com")]
        [InlineData("Lorein", "Johnson", "")]
        [InlineData("Lorein", "Johnson", "exapmle.email$gmail.com")]
        [InlineData("Lorein", "Johnson", "exapmle.emailgmail.com")]
        public void CheckIfValuesValidForUser_ReturnsTrue(string firstName, string lastName, string email)
            => Assert.True(_validator.TestValidate(
                new UserDTO
                {
                    FirstName = firstName, 
                    LastName = lastName, 
                    Email = email
                })
                .IsValid);
    }
}