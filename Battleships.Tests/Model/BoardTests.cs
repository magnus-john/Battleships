using Battleships.Model;
using Battleships.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace Battleships.Tests.Model
{
    public class BoardTests
    {
        private Board _sut = Boards.TenByTen;

        [Theory]
        [InlineData(-1, -1)]
        [InlineData(0, 0)]
        [InlineData(0, Boards.DefaultWidth)]
        [InlineData(Boards.DefaultHeight, 0)]
        public void Initialise_InvalidParameters_ThrowsException(int height, int width)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _sut = new Board(height, width));
        }

        [Fact]
        public void Initialise_ValidParameters_HeightAndWidthAreSetCorrectly()
        {
            _sut = new Board(Boards.DefaultHeight, Boards.DefaultWidth);

            _sut.Height.Should().Be(Boards.DefaultHeight);
            _sut.Width.Should().Be(Boards.DefaultWidth);
        }

        [Fact]
        public void AllShipsAreSunk_NoShipsInFleet_ReturnsFalse()
        {
            _sut.AllShipsAreSunk.Should().BeFalse();
        }

        [Fact]
        public void AllShipsAreSunk_SingleShipInFleet_StillAfloat_ReturnsFalse()
        {
            _sut.Add(Ships.TopLeftTug);
            _sut.AllShipsAreSunk.Should().BeFalse();
        }

        [Fact]
        public void AllShipsAreSunk_SingleShipInFleet_IsSunk_ReturnsTrue()
        {
            var ship = Ships.TopLeftTug;

            _sut.Add(ship);
            _sut.AllShipsAreSunk.Should().BeFalse();

            Ships.Sink(ship);

            _sut.AllShipsAreSunk.Should().BeTrue();
        }

        [Fact]
        public void AllShipsAreSunk_MultipleShipsInFleet_AllStillAfloat_ReturnsFalse()
        {
            _sut.Add(Ships.TopLeftTug);
            _sut.Add(Ships.SecondRowTug);

            _sut.AllShipsAreSunk.Should().BeFalse();
        }

        [Fact]
        public void AllShipsAreSunk_MultipleShipsInFleet_MixtureOfSunkAndAfloat_ReturnsFalse()
        {
            _sut.Add(Ships.TopLeftTug);
            _sut.AllShipsAreSunk.Should().BeFalse();

            var sunk = Ships.SecondRowTug;
            _sut.Add(sunk);

            _sut.AllShipsAreSunk.Should().BeFalse();

            Ships.Sink(sunk);

            _sut.AllShipsAreSunk.Should().BeFalse();
        }

        [Fact]
        public void AllShipsAreSunk_MultipleShipsInFleet_AllSunk_ReturnsTrue()
        {
            var ships = new List<Ship>
            {
                Ships.TopLeftTug, 
                Ships.SecondRowTug
            };

            ships.ForEach(x => _sut.Add(x));

            _sut.AllShipsAreSunk.Should().BeFalse();

            ships.ForEach(Ships.Sink);

            _sut.AllShipsAreSunk.Should().BeTrue();
        }

        [Theory]
        [ClassData(typeof(BoundaryData))]
        public void IsOutOfBounds_PointIsOutOfBounds_ReturnsTrue(Point p)
        {
            _sut.IsOutOfBounds(p).Should().BeTrue();
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(0, Boards.DefaultHeight - 1)]
        [InlineData(Boards.DefaultWidth - 1, 0)]
        [InlineData(Boards.DefaultWidth - 1, Boards.DefaultHeight - 1)]
        public void IsOutOfBounds_PointIsWithinBounds_ReturnsFalse(int x, int y)
        {
            _sut.IsOutOfBounds(new Point(x, y)).Should().BeFalse();
        }

        [Fact]
        public void FreeSpaces_NoShipsPresent_ReturnsEntireBoard()
        {
            var result = _sut.FreeSpaces.ToList();

            result.Count.Should().Be(Boards.DefaultHeight * Boards.DefaultWidth);
        }

        [Fact]
        public void FreeSpaces_ShipsPresent_ReturnsEntireBoardExceptShipData()
        {
            var ships = new List<Ship>
            {
                Ships.TopLeftDestroyer,
                Ships.SecondRowTug
            };

            ships.ForEach(x => _sut.Add(x));

            var expected = (Boards.DefaultHeight * Boards.DefaultWidth) - ships.SelectMany(x => x.CoOrdinates).Count();
            var result = _sut.FreeSpaces.ToList();

            result.Count.Should().Be(expected);

            ships.ForEach(x => result.Should().NotContain(x.CoOrdinates));
        }

        [Fact]
        public void Hits_NoShipsPresent_IsEmpty()
        {
            _sut.Hits.Should().BeEmpty();
        }

        [Fact]
        public void Hits_ShipsPresent_WithNoHits_IsEmpty()
        {
            _sut.Add(Ships.TopLeftDestroyer);

            _sut.Hits.Should().BeEmpty();
        }

        [Fact]
        public void Hits_ShipsPresent_WithHits_ReturnsHits()
        {
            var ship = Ships.TopLeftDestroyer;
            var first = ship.CoOrdinates.First();
            ship.RecordHit(first);
            _sut.Add(ship);

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
            _sut.RecordMiss(Points.TopLeft);

            _sut.Misses.Should().BeEquivalentTo([Points.TopLeft]);
        }

        [Fact]
        public void Misses_MovesMade_ThatRepeatedlyMiss_ReturnsExpectedResult()
        {
            _sut.RecordMiss(Points.TopLeft);
            _sut.RecordMiss(Points.TopLeft);

            _sut.Misses.Should().BeEquivalentTo([Points.TopLeft]);
        }

        [Fact]
        public void Misses_MovesMade_ThatAllHit_IsEmpty()
        {
            var ship = Ships.TopLeftTug;
            _sut.Add(ship);
            _sut.RecordMiss(ship.CoOrdinates.First());

            _sut.Misses.Should().BeEmpty();
        }

        [Fact]
        public void Misses_MovesMade_MixOfHitsAndMisses_ReturnsExpectedResult()
        {
            var ship = Ships.TopLeftDestroyer;
            var miss = Points.BottomRight(_sut);

            ship.RecordHit(ship.CoOrdinates.First());
            _sut.Add(ship);
            _sut.RecordMiss(miss);

            _sut.Misses.Should().BeEquivalentTo([miss]);
        }

        [Fact]
        public void ShipLocations_FleetIsEmpty_IsEmpty()
        {
            _sut.ShipLocations.Should().BeEmpty();
        }

        [Fact]
        public void ShipLocations_FleetIsPopulated_AndAfloat_ReturnsExpectedResult()
        {
            var ship = Ships.TopLeftDestroyer;
            _sut.Add(ship);

            _sut.ShipLocations.Should().BeEquivalentTo(ship.CoOrdinates);
        }

        [Fact]
        public void ShipLocations_FleetIsPopulated_AndSunk_ReturnsExpectedResult()
        {
            var ship = Ships.TopLeftDestroyer;
            Ships.Sink(ship);
            _sut.Add(ship);

            _sut.ShipLocations.Should().BeEquivalentTo(ship.CoOrdinates);
        }

        [Fact]
        public void UndiscoveredShipLocations_FleetIsEmpty_IsEmpty()
        {
            _sut.UndiscoveredShipLocations.Should().BeEmpty();
        }

        [Fact]
        public void UndiscoveredShipLocations_FleetIsPopulated_AllAfloat_ReturnsExpectedResult()
        {
            var ship = Ships.TopLeftDestroyer;
            _sut.Add(ship);

            _sut.UndiscoveredShipLocations.Should().BeEquivalentTo(ship.CoOrdinates);
        }

        [Fact]
        public void UndiscoveredShipLocations_FleetIsPopulated_PartiallyAfloat_ReturnsExpectedResult()
        {
            var ship = Ships.TopLeftDestroyer;
            var hit = ship.CoOrdinates.First();
            ship.RecordHit(hit);
            _sut.Add(ship);

            _sut.UndiscoveredShipLocations.Should().BeEquivalentTo(ship.CoOrdinates.Except([hit]));
        }

        [Fact]
        public void UndiscoveredShipLocations_FleetIsPopulated_MixOfAfloatAndSunk_ReturnsExpectedResult()
        {
            var afloat = Ships.TopLeftDestroyer;
            var sunk = Ships.SecondRowTug;
            Ships.Sink(sunk);
            _sut.Add(afloat);
            _sut.Add(sunk);

            _sut.UndiscoveredShipLocations.Should().BeEquivalentTo(afloat.CoOrdinates);
        }

        [Fact]
        public void UndiscoveredShipLocations_FleetIsPopulated_AllSunk_IsEmpty()
        {
            var ship = Ships.TopLeftDestroyer;
            Ships.Sink(ship);
            _sut.Add(ship);

            _sut.UndiscoveredShipLocations.Should().BeEmpty();
        }

        [Fact]
        public void Add_ShipIsWithinBoundsAndFits_ShipIsAddedToBoard()
        {
            _sut.ShipLocations.Should().BeEmpty();

            var result = _sut.Add(Ships.TopLeftTug);

            result.Should().BeTrue();
            _sut.ShipLocations.Should().BeEquivalentTo([Points.TopLeft]);
        }

        [Fact]
        public void Add_ShipIsWithinBoundsAndDoesNotFit_ShipIsNotAddedToBoard()
        {
            _sut.ShipLocations.Should().BeEmpty();

            var result = _sut.Add(Ships.TopLeftTooLong);

            result.Should().BeFalse();
            _sut.ShipLocations.Should().BeEmpty();
        }

        [Fact]
        public void Add_ShipIsWithinBoundsAndOverlapsWithExistingShip_ShipIsNotAddedToBoard()
        {
            _sut.ShipLocations.Should().BeEmpty();

            var ship = Ships.TopLeftTug;
            var result = _sut.Add(ship);

            result.Should().BeTrue();
            _sut.ShipLocations.Should().BeEquivalentTo(ship.CoOrdinates);

            result = _sut.Add(Ships.TopLeftDestroyer);

            result.Should().BeFalse();
            _sut.ShipLocations.Should().BeEquivalentTo(ship.CoOrdinates);
        }

        [Fact]
        public void Add_ShipIsOutOfBoundsAndFits_ShipIsNotAddedToBoard()
        {
            _sut.ShipLocations.Should().BeEmpty();

            var result = _sut.Add(Ships.OutOfBoundsTug);

            result.Should().BeFalse();
            _sut.ShipLocations.Should().BeEmpty();
        }

        [Fact]
        public void RecordMiss_PointIsOutsideBounds_MissIsNotRecorded()
        {
            _sut.Misses.Should().BeEmpty();

            var result = _sut.RecordMiss(Points.OutOfBounds);

            result.Should().BeFalse();
            _sut.Misses.Should().BeEmpty();
        }

        [Fact]
        public void RecordMiss_PointOverlapsWithExistingShip_MissIsNotRecorded()
        {
            _sut.Misses.Should().BeEmpty();
            _sut.ShipLocations.Should().BeEmpty();

            var result = _sut.Add(Ships.TopLeftTug);

            result.Should().BeTrue();
            _sut.Misses.Should().BeEmpty();
            _sut.ShipLocations.Should().BeEquivalentTo([Points.TopLeft]);

            result = _sut.RecordMiss(Points.TopLeft);

            result.Should().BeFalse();
            _sut.Misses.Should().BeEmpty();
            _sut.ShipLocations.Should().BeEquivalentTo([Points.TopLeft]);
        }

        [Fact]
        public void RecordMiss_PointIsWithinBoundsAndDoesNotOverlapWithExistingShip_MissIsRecorded()
        {
            _sut.Misses.Should().BeEmpty();

            var location = Points.TopLeft;
            var result = _sut.RecordMiss(location);

            result.Should().BeTrue();
            _sut.Misses.Should().BeEquivalentTo([location]);
        }

        [Fact]
        public void ShipAt_NoShipExistsAtLocation_ReturnsNull()
        {
            var result = _sut.ShipAt(Points.TopLeft);

            result.Should().BeNull();
        }

        [Fact]
        public void ShipAt_ShipExistsAtLocation_ReturnsShip()
        {
            var ship = Ships.TopLeftTug;

            _sut.Add(ship);

            var result = _sut.ShipAt(ship.CoOrdinates.First());

            result.Should().Be(ship);
        }
        
        public class BoundaryData : TheoryData<Point>
        {
            public BoundaryData()
            {
                Add(new Point(-1, -1));
                Add(new Point(-1, 0));
                Add(new Point(0, -1));
                Add(new Point(Boards.DefaultWidth, 0));
                Add(new Point(0, Boards.DefaultHeight));
                Add(new Point(Boards.DefaultWidth, Boards.DefaultHeight));
            }
        }
    }
}
