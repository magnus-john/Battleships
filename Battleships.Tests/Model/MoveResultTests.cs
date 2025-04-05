using Battleships.Model;
using Battleships.Model.Enums;
using FluentAssertions;
using Xunit;

namespace Battleships.Tests.Model
{
    public class MoveResultTests
    {
        private const string ShipName = "ShipName";

        [Theory]
        [InlineData(MoveOutcome.None)]
        [InlineData(MoveOutcome.Hit, ShipName)]
        [InlineData(MoveOutcome.Invalid)]
        [InlineData(MoveOutcome.Miss, ShipName)]
        [InlineData(MoveOutcome.Sink)]
        [InlineData(MoveOutcome.OutOfBounds, ShipName)]
        public void Initialise_PropertiesAreSetCorrectly(MoveOutcome outcome, string? sunk = null)
        {
            var result = new MoveResult(outcome, sunk);

            result.Outcome.Should().Be(outcome);
            result.Sunk.Should().Be(sunk);
        }
    }
}
