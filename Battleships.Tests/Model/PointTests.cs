using Battleships.Model;
using FluentAssertions;
using Xunit;

namespace Battleships.Tests.Model
{
    public class PointTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(-1, -1)]
        [InlineData(1, 1)]
        [InlineData(100, -100)]
        [InlineData(int.MaxValue, int.MinValue)]
        public void Initialise_ValuesAreSetCorrectly(int x, int y)
        {
            var result = new Point(x, y);

            result.X.Should().Be(x);
            result.Y.Should().Be(y);
        }
    }
}
