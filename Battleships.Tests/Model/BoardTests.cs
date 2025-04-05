using Battleships.Model;
using Battleships.Model.Enums;
using Battleships.Services.Exceptions;
using Battleships.Tests.Extensions;
using Battleships.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace Battleships.Tests.Model
{
    public class BoardTests
    {
        private Board _sut = Boards.MediumWithTopLeftTug;

        [Fact]
        public void Initialise_ShipsEmpty_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _sut = new Board(new MediumBoard(), []));
        }

        [Fact]
        public void Initialise_ShipsOverlap_ThrowsException()
        {
            Assert.Throws<CannotPlaceShipException>(() => _sut = new Board(new MediumBoard(), [Ships.TopLeftTug, Ships.TopLeftTug]));
        }

        [Fact]
        public void Initialise_ShipsOutOfBounds_ThrowsException()
        {
            Assert.Throws<CannotPlaceShipException>(() => _sut = new Board(new MediumBoard(), [Ships.OutOfBoundsTug]));
        }

        [Fact]
        public void Initialise_ValidParameters_HeightAndWidthAreSetCorrectly()
        {
            var template = new MediumBoard();

            _sut = new Board(template, [Ships.TopLeftTug]);

            _sut.Height.Should().Be(template.Height);
            _sut.Width.Should().Be(template.Width);
        }

        [Fact]
        public void AllShipsAreSunk_SingleShipInFleet_StillAfloat_ReturnsFalse()
        {
            _sut.AllShipsAreSunk.Should().BeFalse();
        }

        [Fact]
        public void AllShipsAreSunk_SingleShipInFleet_IsSunk_ReturnsTrue()
        {
            var ship = Ships.TopLeftTug;
            Ships.Sink(ship);
            _sut = Boards.Medium([ship]);

            _sut.AllShipsAreSunk.Should().BeTrue();
        }

        [Fact]
        public void AllShipsAreSunk_MultipleShipsInFleet_AllStillAfloat_ReturnsFalse()
        {
            _sut = Boards.Medium(TwoShips);

            _sut.AllShipsAreSunk.Should().BeFalse();
        }

        [Fact]
        public void AllShipsAreSunk_MultipleShipsInFleet_MixtureOfSunkAndAfloat_ReturnsFalse()
        {
            var ships = TwoShips;
            Ships.Sink(ships.First());
            _sut = Boards.Medium(ships);

            _sut.AllShipsAreSunk.Should().BeFalse();
        }

        [Fact]
        public void AllShipsAreSunk_MultipleShipsInFleet_AllSunk_ReturnsTrue()
        {
            var ships = TwoShips;
            ships.ForEach(Ships.Sink);
            _sut = Boards.Medium(ships);

            _sut.AllShipsAreSunk.Should().BeTrue();
        }

        [Fact]
        public void Hits_ShipsPresent_WithNoHits_IsEmpty()
        {
            _sut.Hits.Should().BeEmpty();
        }

        [Fact]
        public void Hits_ShipsPresent_WithHits_ReturnsHits()
        {
            var ship = Ships.TopLeftDestroyer;
            var first = ship.CoOrdinates.First();
            ship.RecordHit(first);

            _sut = Boards.Medium([ship]);
            _sut.Hits.Should().BeEquivalentTo([first]);

            Ships.Sink(ship);

            _sut.Hits.Should().BeEquivalentTo(ship.CoOrdinates);
        }

        [Fact]
        public void Misses_NoMovesMade_IsEmpty()
        {
            _sut.Misses.Should().BeEmpty();
        }
        
        [Fact]
        public void Misses_MovesMade_ThatMiss_ReturnsExpectedResult()
        {
            var expected = _sut.FreeSpaces().First();
            _sut.FireUpon(expected);

            _sut.Misses.Should().BeEquivalentTo([expected]);
        }

        [Fact]
        public void Misses_MovesMade_ThatRepeatedlyMiss_ReturnsExpectedResult()
        {
            var expected = _sut.FreeSpaces().First();
            _sut.FireUpon(expected);
            _sut.FireUpon(expected);

            _sut.Misses.Should().BeEquivalentTo([expected]);
        }

        [Fact]
        public void Misses_MovesMade_ThatAllHit_IsEmpty()
        {
            _sut.FireUpon(_sut.ShipLocations.First());

            _sut.Misses.Should().BeEmpty();
        }

        [Fact]
        public void Misses_MovesMade_MixOfHitsAndMisses_ReturnsExpectedResult()
        {
            _sut = Boards.MediumWithTopLeftDestroyer;
            _sut.FireUpon(_sut.ShipLocations.First());
            var expected = _sut.FreeSpaces().First();
            _sut.FireUpon(expected);

            _sut.Misses.Should().BeEquivalentTo([expected]);
        }

        [Fact]
        public void ShipLocations_FleetIsPopulated_AndAfloat_ReturnsExpectedResult()
        {
            var ship = Ships.TopLeftDestroyer;
            _sut = Boards.Medium([ship]);

            _sut.ShipLocations.Should().BeEquivalentTo(ship.CoOrdinates);
        }

        [Fact]
        public void ShipLocations_FleetIsPopulated_AndSunk_ReturnsExpectedResult()
        {
            var ship = Ships.TopLeftDestroyer;
            Ships.Sink(ship);
            _sut = Boards.Medium([ship]);

            _sut.ShipLocations.Should().BeEquivalentTo(ship.CoOrdinates);
        }

        [Fact]
        public void UndiscoveredShipLocations_FleetIsAllAfloat_ReturnsExpectedResult()
        {
            var ship = Ships.TopLeftDestroyer;
            _sut = Boards.Medium([ship]);

            _sut.UndiscoveredShipLocations.Should().BeEquivalentTo(ship.CoOrdinates);
        }

        [Fact]
        public void UndiscoveredShipLocations_FleetIsPartiallyAfloat_ReturnsExpectedResult()
        {
            var ship = Ships.TopLeftDestroyer;
            var hit = ship.CoOrdinates.First();
            ship.RecordHit(hit);
            _sut = Boards.Medium([ship]);

            _sut.UndiscoveredShipLocations.Should().BeEquivalentTo(ship.CoOrdinates.Except([hit]));
        }

        [Fact]
        public void UndiscoveredShipLocations_FleetIsAMixOfAfloatAndSunk_ReturnsExpectedResult()
        {
            var ships = TwoShips;
            Ships.Sink(ships.First());
            var afloat = ships.Last();
            _sut = Boards.Medium(ships);

            _sut.UndiscoveredShipLocations.Should().BeEquivalentTo(afloat.CoOrdinates);
        }

        [Fact]
        public void UndiscoveredShipLocations_FleetIsAllSunk_IsEmpty()
        {
            var ship = Ships.TopLeftDestroyer;
            Ships.Sink(ship);
            _sut = Boards.Medium([ship]);

            _sut.UndiscoveredShipLocations.Should().BeEmpty();
        }

        [Fact]
        public void Attack_TargetIsOutOfBounds_ReturnsOutOfBounds()
        {
            var result = _sut.FireUpon(Points.OutOfBounds);

            result.Should().BeEquivalentTo(new MoveResult(MoveOutcome.OutOfBounds));
        }

        [Fact]
        public void Attack_TargetMisses_ReturnsMiss()
        {
            var result = _sut.FireUpon(_sut.FreeSpaces().First());

            result.Should().BeEquivalentTo(new MoveResult(MoveOutcome.Miss));
        }

        [Fact]
        public void Attack_TargetHits_AndDoesNotSinkShip_ReturnsHit()
        {
            _sut = Boards.MediumWithTopLeftDestroyer;
            var result = _sut.FireUpon(_sut.ShipLocations.First());

            result.Should().BeEquivalentTo(new MoveResult(MoveOutcome.Hit));
        }

        [Fact]
        public void Attack_TargetHits_AndSinksShip_ReturnsSink()
        {
            _sut = Boards.MediumWithTopLeftTug;
            var result = _sut.FireUpon(_sut.ShipLocations.First());

            result.Should().BeEquivalentTo(new MoveResult(MoveOutcome.Sink, nameof(Tug)));
        }

        private static List<Ship> TwoShips => 
        [
            Ships.TopLeftTug,
            Ships.SecondRowTug
        ];
    }
}
