using Battleships.Model;
using Battleships.Model.Enums;
using Battleships.Tests.Helpers;
using FluentAssertions;
using Xunit;
#pragma warning disable xUnit1045
#pragma warning disable xUnit1044

namespace Battleships.Tests.Model
{
    public class BoardTemplateTests
    {
        [Theory]
        [InlineData(typeof(SmallBoard), BoardTemplate.Small)]
        [InlineData(typeof(MediumBoard), BoardTemplate.Medium)]
        [InlineData(typeof(LargeBoard), BoardTemplate.Large)]
        public void Initialise_HasPropertiesSetCorrectly(Type type, int size)
        {
            var board = Activator.CreateInstance(type) as BoardTemplate;

            board.Should().NotBeNull();
            board.Height.Should().Be(size);
            board.Width.Should().Be(size);

            var locations = board.Locations.ToHashSet();

            locations.Count.Should().Be(size * size);

            foreach (var x in Enumerable.Range(0, size))
            foreach (var y in Enumerable.Range(0, size))
            {
                locations.Should().Contain(new Point(x, y));
            }
        }

        [Theory]
        [MemberData(nameof(ShipsThatDoNotFit))]
        public void Fits_ShipDoesNotFits_ReturnsFalse(BoardTemplate template, Ship ship)
        {
            template.Fits(ship).Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(ShipsThatFit))]
        public void Fits_ShipFits_ReturnsTrue(BoardTemplate template, Ship ship)
        {
            template.Fits(ship).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(PointsOutsideTemplateBounds))]
        public void IsOutOfBounds_PointIsOutOfBounds_ReturnsTrue(BoardTemplate template, Point point)
        {
            template.IsOutOfBounds(point).Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(PointsWithinTemplateBounds))]
        public void IsOutOfBounds_PointIsWithinBounds_ReturnsFalse(BoardTemplate template, Point point)
        {
            template.IsOutOfBounds(point).Should().BeFalse();
        }

        public static TheoryData<BoardTemplate, Ship> ShipsThatDoNotFit => new()
        {
            { BoardTemplates.Tiny, Ships.OutOfBoundsTug },
            { BoardTemplates.Medium, Ships.OutOfBoundsTug },
            { BoardTemplates.Medium, Ships.Destroyer(new Point(7, 0), Orientation.Horizontal) },
            { BoardTemplates.Medium, Ships.Destroyer(new Point(7, 9), Orientation.Horizontal) },
            { BoardTemplates.Medium, Ships.Destroyer(new Point(0, 7), Orientation.Vertical) },
            { BoardTemplates.Medium, Ships.Destroyer(new Point(9, 7), Orientation.Horizontal) },
            { BoardTemplates.Medium, Ships.Destroyer(new Point(7, 7), Orientation.Horizontal) },
            { BoardTemplates.Medium, Ships.Destroyer(new Point(7, 7), Orientation.Vertical) }
        };

        public static TheoryData<BoardTemplate, Ship> ShipsThatFit => new()
        {
            { BoardTemplates.Tiny, Ships.TopLeftTug },
            { BoardTemplates.Medium, Ships.TopLeftTug },
            { BoardTemplates.Medium, Ships.TopLeftDestroyer },
            { BoardTemplates.Medium, Ships.Destroyer(new Point(6, 0), Orientation.Horizontal) },
            { BoardTemplates.Medium, Ships.Destroyer(new Point(6, 9), Orientation.Horizontal) },
            { BoardTemplates.Medium, Ships.Destroyer(new Point(0, 6), Orientation.Vertical) },
            { BoardTemplates.Medium, Ships.Destroyer(new Point(6, 0), Orientation.Horizontal) },
            { BoardTemplates.Medium, Ships.Destroyer(new Point(9, 6), Orientation.Vertical) },
            { BoardTemplates.Medium, Ships.Destroyer(new Point(6, 6), Orientation.Horizontal) },
            { BoardTemplates.Medium, Ships.Destroyer(new Point(6, 6), Orientation.Vertical) }
        };

        public static TheoryData<BoardTemplate, Point> PointsOutsideTemplateBounds => new()
        {
            { BoardTemplates.Medium, Points.OutOfBounds },
            { BoardTemplates.Medium, new Point(0, -1) },
            { BoardTemplates.Medium, new Point(-1, 0) },
            { BoardTemplates.Medium, new Point(0, BoardTemplate.Medium) },
            { BoardTemplates.Medium, new Point(BoardTemplate.Medium, 0) },
            { BoardTemplates.Medium, new Point(BoardTemplate.Medium, BoardTemplate.Medium) },
            { BoardTemplates.Medium, new Point(5, BoardTemplate.Medium) },
            { BoardTemplates.Medium, new Point(BoardTemplate.Medium, 5) },
        };

        public static TheoryData<BoardTemplate, Point> PointsWithinTemplateBounds => new()
        {
            { BoardTemplates.Medium, Points.TopLeft },
            { BoardTemplates.Medium, Points.TopRight(BoardTemplates.Medium) },
            { BoardTemplates.Medium, Points.BottomLeft(BoardTemplates.Medium) },
            { BoardTemplates.Medium, Points.BottomRight(BoardTemplates.Medium) },
            { BoardTemplates.Medium, new Point(0, 5) },
            { BoardTemplates.Medium, new Point(9, 5) },
            { BoardTemplates.Medium, new Point(5, 0) },
            { BoardTemplates.Medium, new Point(5, 9) },
            { BoardTemplates.Medium, new Point(5, 5) },
        };
    }
}
