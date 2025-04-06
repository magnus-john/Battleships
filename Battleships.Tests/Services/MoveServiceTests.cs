using Battleships.Model;
using Battleships.Model.Enums;
using Battleships.Services;
using Battleships.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace Battleships.Tests.Services
{
    public class MoveServiceTests
    {
        private readonly MoveService _sut = new();

        [Fact]
        public void MakeMove_MoveIsInvalid_ReturnsExpectedResult()
        {
            var board = Boards.TinyWithTopLeftTug;
            var move = Moves.Invalid;
            var expected = new ActionResult(Outcome.Invalid);

            var response = _sut.MakeMove(board, move);

            response.Board.Should().Be(board);
            response.GameIsOver.Should().BeFalse();
            response.Result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void MakeMove_MoveIsValid_ReturnsExpectedResult()
        {
            var board = Boards.TinyWithTopLeftTug;
            var move = Moves.A1;
            var expected = new ActionResult(Outcome.Sink, nameof(Tug));

            var response = _sut.MakeMove(board, move);

            response.Board.Should().Be(board);
            response.GameIsOver.Should().BeTrue();
            response.Result.Should().BeEquivalentTo(expected);
        }
    }
}
