using FluentValidation.TestHelper;
using SchoolLibrary_EF.API.Validation;
using SchoolLibrary_EF.BLL.DTOs.UserDTOs;
using Xunit;

namespace SchoolLibrary_EF.Tests.Validation
{
    public class UserDTO_ValidatorTests
    {
        private readonly InsertDTO_User_Validator _validator = new InsertDTO_User_Validator();

        [Theory]
        [InlineData("Lorein", "Johnson", "street", "city", "country")]
        [InlineData("", "Johnson", "street", "city", "country")]
        [InlineData("Lorein", "", "street", "city", "country")]
        [InlineData("Lorein", "Johnson", "", "city", "country")]
        [InlineData("Lorein", "Johnson", "street", "", "country")]
        [InlineData("Lorein", "Johnson", "street", "city", "")]
        public void CheckIfValuesValidForUser_ReturnsTrue
            (string firstName, string lastName, string street, string city, string country)
        {
            var user = new InsertDTO_User()
            {
                FirstName = firstName,
                LastName = lastName,
                Street = street,
                City = city,
                Country = country,
            };
            
            Assert.True(_validator.TestValidate(user).IsValid);
        }
    }
}