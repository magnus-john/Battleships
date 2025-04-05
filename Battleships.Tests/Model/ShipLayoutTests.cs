using Battleships.Model;
using Battleships.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace Battleships.Tests.Model
{
    public class ShipLayoutTests
    {
        [Fact]
        public void Battleship_LengthIsSetCorrectly()
        {
            ShipLayouts.Battleship.Length.Should().Be(ShipLayout.BattleshipLength);
        }

        [Fact]
        public void Destroyer_LengthIsSetCorrectly()
        {
            ShipLayouts.Destroyer.Length.Should().Be(ShipLayout.DestroyerLength);
        }
    }
}
