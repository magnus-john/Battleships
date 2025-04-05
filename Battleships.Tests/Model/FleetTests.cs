using Battleships.Model;
using Battleships.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace Battleships.Tests.Model
{
    public class FleetTests
    {
        [Fact]
        public void Armada_HasCorrectShips()
        {
            var fleet = Fleets.Armada;

            fleet.Count.Should().Be(5);
            fleet.OfType<Battleship>().Count().Should().Be(2);
            fleet.OfType<Destroyer>().Count().Should().Be(3);
        }

        [Fact]
        public void Flotilla_HasCorrectShips()
        {
            var fleet = Fleets.Flotilla;

            fleet.Count.Should().Be(2);
            fleet.OfType<Battleship>().Count().Should().Be(1);
            fleet.OfType<Destroyer>().Count().Should().Be(1);
        }

        [Fact]
        public void Squadron_HasCorrectShips()
        {
            var fleet = Fleets.Squadron;

            fleet.Count.Should().Be(3);
            fleet.OfType<Battleship>().Count().Should().Be(1);
            fleet.OfType<Destroyer>().Count().Should().Be(2);
        }
    }
}
