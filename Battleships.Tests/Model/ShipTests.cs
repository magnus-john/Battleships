using Battleships.Model;
using Battleships.Model.Enums;
using Battleships.Tests.Helpers;
using FluentAssertions;
using Xunit;
#pragma warning disable xUnit1045
#pragma warning disable xUnit1044

namespace Battleships.Tests.Model
{
    public class ShipTests
    {
        [Theory]
        [InlineData(typeof(Battleship), nameof(Battleship))]
        [InlineData(typeof(Destroyer), nameof(Destroyer))]
        [InlineData(typeof(Tug), nameof(Tug))]
        public void Initialise_NameIsSetCorrectly(Type type, string name)
        {
            var instance = Activator.CreateInstance(type) as ShipLayout;

            instance.Should().NotBeNull();

            var result = new Ship(instance, Points.TopLeft, Orientation.Horizontal);

            result.Name.Should().Be(name);
        }

        [Theory]
        [MemberData(nameof(CoOrdinateData))]
        public void Initialise_CoOrdinatesAreSetCorrectly(ShipLayout layout, Point point, Orientation orientation, Point[] expected)
        {
            var result = new Ship(layout, point, orientation);

            result.CoOrdinates.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(Orientation.Horizontal)]
        [InlineData(Orientation.Vertical)]
        public void Initialise_LayoutHasNoLength_ThrowsExpectedException(Orientation orientation)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Ship(ShipLayouts.Ghost, Points.TopLeft, orientation));
        }

        [Fact]
        public void HitType_DoesNotHit_ReturnsMiss()
        {
            var ship = Ships.TopLeftTug;
            var first = ship.RecordHit(Points.OutOfBounds);
            var second = ship.RecordHit(Points.SecondRow);

            first.Should().Be(HitType.Miss);
            second.Should().Be(HitType.Miss);
        }

        [Fact]
        public void HitType_RepeatHit_ReturnsRepeat()
        {
            var ship = Ships.TopLeftTug;
            _ = ship.RecordHit(Points.TopLeft);
            var second = ship.RecordHit(Points.TopLeft);

            second.Should().Be(HitType.Repeat);
        }

        [Fact]
        public void HitType_Fatal_ReturnsFatal()
        {
            var ship = Ships.TopLeftTug;
            var fatal = ship.RecordHit(Points.TopLeft);

            fatal.Should().Be(HitType.Fatal);
        }

        [Fact]
        public void HitType_NonFatal_ReturnsNonFatal()
        {
            var ship = Ships.TopLeftDestroyer;
            var nonFatal = ship.RecordHit(Points.TopLeft);

            nonFatal.Should().Be(HitType.NonFatal);
        }

        [Fact]
        public void IsSunk_ShipIsSunk_ReturnsTrue()
        {
            var ship = Ships.TopLeftTug;
            Ships.Sink(ship);

            ship.IsSunk.Should().BeTrue();
        }

        [Fact]
        public void IsSunk_ShipIsNotSunk_ReturnsFalse()
        {
            Ships.TopLeftTug.IsSunk.Should().BeFalse();
        }

        public static TheoryData<ShipLayout, Point, Orientation, Point[]> CoOrdinateData => new()
        {
            { ShipLayouts.Battleship, Points.TopLeft, Orientation.Horizontal, [Points.TopLeft, new Point(1, 0), new Point(2, 0), new Point(3, 0), new Point(4, 0)] },
            { ShipLayouts.Battleship, Points.TopLeft, Orientation.Vertical, [Points.TopLeft, new Point(0, 1), new Point(0, 2), new Point(0, 3), new Point(0, 4)] },
            { ShipLayouts.Destroyer, Points.TopLeft, Orientation.Horizontal, [Points.TopLeft, new Point(1, 0), new Point(2, 0), new Point(3, 0)] },
            { ShipLayouts.Destroyer, Points.TopLeft, Orientation.Vertical, [Points.TopLeft, new Point(0, 1), new Point(0, 2), new Point(0, 3)] },
            { ShipLayouts.Tug, Points.TopLeft, Orientation.Horizontal, [Points.TopLeft] },
            { ShipLayouts.Tug, Points.TopLeft, Orientation.Vertical, [Points.TopLeft] }
        };
    }
}
