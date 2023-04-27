using Application.System.Commands.SeedBogusData;
using Domain.Common;
using Xunit;

namespace Application.UnitTests.System.Commands.SeedBogusData
{
    public class DataGeneratorTests
    {
        [Fact]
        public void CheckGeneratedUserToNull_True()
        {
            DataGenerator.InitBogusData();
            
            var user = DataGenerator.Users.FirstOrDefault();
            
            Assert.True(user != null);
        }

        [Fact]
        public void CheckGeneratedUserNameToNull_True()
        {
            DataGenerator.InitBogusData();

            var userName = DataGenerator.Users.First().UserName;
            
            Assert.True(userName != null!);
        }

        [Fact]
        public void CheckGeneratedUserNameNameToNull_True()
        {
            DataGenerator.InitBogusData();

            var name = DataGenerator.Users.First().UserName.FirstName;
            
            Assert.True(name != null!);
        }
    }
}