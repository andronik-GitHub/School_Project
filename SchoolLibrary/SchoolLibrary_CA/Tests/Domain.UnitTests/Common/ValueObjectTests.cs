using Domain.Common;
using Xunit;

namespace Domain.UnitTests.Common
{
    public class ValueObjectTests
    {
        [Fact]
        public void Equals_GivenDifferentValues_False()
        {
            var vo1 = new VO(1, 2);
            var vo2 = new VO(2, 1);

            Assert.False(vo1.Equals(vo2));
        }

        [Fact]
        public void Equals_GivenMatchingValues_True()
        {
            var vo1 = new VO(1, 2);
            var vo2 = new VO(2, 1);

            Assert.False(vo1.Equals(vo2));
        }
        
        
        private class VO : ValueObject 
        {
            private int X { get; }
            private int Y { get; }
            
            private VO() { }

            public VO(int x, int y)
            {
                X = x;
                Y = y;
            }

            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return X;
                yield return Y;
            }
        }
    }
}