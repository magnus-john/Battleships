using Battleships.Model;
using Battleships.Model.Enums;
using FluentAssertions;
using Xunit;

namespace Battleships.Tests.Model
{
    public class ActionResultTests
    {
        private const string ShipName = "ShipName";

        [Theory]
        [InlineData(Outcome.None)]
        [InlineData(Outcome.Hit, ShipName)]
        [InlineData(Outcome.Invalid)]
        [InlineData(Outcome.Miss, ShipName)]
        [InlineData(Outcome.Sink)]
        [InlineData(Outcome.OutOfBounds, ShipName)]
        public void Initialise_PropertiesAreSetCorrectly(Outcome outcome, string? sunk = null)
        {
            var result = new ActionResult(outcome, sunk);

            result.Outcome.Should().Be(outcome);
            result.Sunk.Should().Be(sunk);
        }
    }
}
