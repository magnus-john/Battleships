using Battleships.Model.Enums;
using Battleships.Services;
using Battleships.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace Battleships.Tests.Services
{
    public class BoardDisplayServiceTests
    {
        private readonly BoardDisplayService _sut = new();

        [Fact]
        public void GetData_OneUndiscoveredShip_ReturnsExpectedResult()
        {
            var board = Boards.VerySmall([Ships.TopLeftTug]);
            var result = _sut.GetData(board);

            result.GetLength(0).Should().Be(2);
            result.GetLength(1).Should().Be(2);
            result.Should().BeEquivalentTo(new[,]
            {
                { BoardElement.Undiscovered, BoardElement.Empty },
                { BoardElement.Empty, BoardElement.Empty }
            });
        }

        [Fact]
        public void GetData_TwoUndiscoveredShips_ReturnsExpectedResult()
        {
            var board = Boards.VerySmall([Ships.TopLeftTug, Ships.SecondRowTug]);
            var result = _sut.GetData(board);

            result.GetLength(0).Should().Be(2);
            result.GetLength(1).Should().Be(2);
            result.Should().BeEquivalentTo(new[,]
            {
                { BoardElement.Undiscovered, BoardElement.Undiscovered },
                { BoardElement.Empty, BoardElement.Empty }
            });
        }

        [Fact]
        public void GetData_TwoShips_OneSunkOneAfloat_ReturnsExpectedResult()
        {
            var board = Boards.VerySmall([Ships.TopLeftTug, Ships.SecondRowTug]);
            board.FireUpon(Points.TopLeft);
            var result = _sut.GetData(board);

            result.GetLength(0).Should().Be(2);
            result.GetLength(1).Should().Be(2);

            result.Should().BeEquivalentTo(new[,]
            {
                { BoardElement.Hit, BoardElement.Undiscovered },
                { BoardElement.Empty, BoardElement.Empty }
            });
        }

        [Fact]
        public void GetData_OneShip_AndSomeMisses_ReturnsExpectedResult()
        {
            var board = Boards.VerySmall([Ships.TopLeftTug]);
            board.FireUpon(Points.A2);
            board.FireUpon(Points.B1);
            board.FireUpon(Points.B2);
            var result = _sut.GetData(board);

            result.GetLength(0).Should().Be(2);
            result.GetLength(1).Should().Be(2);
            result.Should().BeEquivalentTo(new[,]
            {
                { BoardElement.Undiscovered, BoardElement.Miss },
                { BoardElement.Miss, BoardElement.Miss }
            });
        }

        [Fact]
        public void GetData_TwoShips_MixOfHitsAndMisses_ReturnsExpectedResult()
        {
            var board = Boards.VerySmall([Ships.TopLeftTug, Ships.SecondRowTug]);
            board.FireUpon(Points.A2);
            board.FireUpon(Points.B1);
            var result = _sut.GetData(board);

            result.GetLength(0).Should().Be(2);
            result.GetLength(1).Should().Be(2);
            result.Should().BeEquivalentTo(new[,]
            {
                { BoardElement.Undiscovered, BoardElement.Hit },
                { BoardElement.Miss, BoardElement.Empty }
            });
        }
    }
}
