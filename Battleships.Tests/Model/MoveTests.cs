using Battleships.Model;
using Battleships.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace Battleships.Tests.Model
{
    public class MoveTests
    {
        private const string Empty = "";
        private const string Test = "test";

        [Theory]
        [InlineData(Move.Exit, true)]
        [InlineData(Empty, false)]
        [InlineData(Test, false)]
        [InlineData(null, false)]
        public void Initialise_IsExitIsSetCorrectly(string? input, bool expected)
        {
            var result = new Move(input);

            result.IsExit.Should().Be(expected);
        }

        [Theory]
        [MemberData(nameof(LocationData))]
        public void Initialise_LocationIsSetCorrectly(string? input, Point? expected)
        {
            var result = new Move(input);

            result.Location.Should().Be(expected);
        }

        public static TheoryData<string?, Point?> LocationData => new()
        {
            { null, null },
            { Empty, null },
            { Test, null },
            { "a1", Points.TopLeft },
            { "A1", Points.TopLeft },
            { "Z99", new Point(98, 25) },
            { "z9999", new Point(9998, 25) },
            { "b0", new Point(-1, 1) },
            { "A-1", null },
            { $"C{int.MaxValue}", new Point(int.MaxValue - 1, 2) },
            { $"A{(long)(int.MaxValue) + 1}", null },
        };
    }
}
