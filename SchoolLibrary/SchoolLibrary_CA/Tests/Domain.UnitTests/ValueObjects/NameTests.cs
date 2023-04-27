using Domain.ValueObjects;
using Xunit;

namespace Domain.UnitTests.ValueObjects
{
    public class NameTests
    {
        [Theory]
        [InlineData("John")]
        [InlineData("Mike")]
        public void ShouldHaveEqualName_True(string value)
        {
            var name = new Name(value);
            
            Assert.True(name.Equals(new Name("John")));
        }
    }
}