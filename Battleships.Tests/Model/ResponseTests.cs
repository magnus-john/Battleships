using Battleships.Model;
using Battleships.Model.Enums;
using Battleships.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace Battleships.Tests.Model
{
    public class ResponseTests
    {
        private const string Test = "Test";

        [Fact]
        public void Initialise_WithBoard_PropertiesAreSetCorrectly()
        {
            var board = Boards.TinyWithTopLeftTug;
            var result = new Response(board);

            result.Board.Should().Be(board);
            result.Result.Outcome.Should().Be(Outcome.None);
            result.Result.Sunk.Should().BeNull();
        }

        [Theory]
        [ClassData(typeof(OutcomeData))]
        public void Initialise_WithBoardAndOutcome_PropertiesAreSetCorrectly(Outcome outcome)
        {
            var board = Boards.TinyWithTopLeftTug;
            var result = new Response(board, outcome);

            result.Board.Should().Be(board);
            result.Result.Outcome.Should().Be(outcome);
            result.Result.Sunk.Should().BeNull();
        }

        [Theory]
        [ClassData(typeof(OutcomeData))]
        public void Initialise_WithBoardAndResult_PropertiesAreSetCorrectly(Outcome outcome)
        {
            var board = Boards.TinyWithTopLeftTug;
            var result = new Response(board, new ActionResult(outcome, Test));

            result.Board.Should().Be(board);
            result.Result.Outcome.Should().Be(outcome);
            result.Result.Sunk.Should().Be(Test);
        }

        [Fact]
        public void GameIsOver_GameIsNotOver_ReturnsFalse()
        {
            var result = new Response(Boards.TinyWithTopLeftTug);

            result.GameIsOver.Should().BeFalse();
        }

        [Fact]
        public void GameIsOver_GameIsOver_ReturnsTrue()
        {
            var board = Boards.TinyWithTopLeftTug;
            board.FireUpon(Points.TopLeft);
            var result = new Response(board);

            result.GameIsOver.Should().BeTrue();
        }
    }

    public class OutcomeData : TheoryData<Outcome>
    {
        public OutcomeData()
        {
            foreach (var outcome in Enum.GetValues<Outcome>())
            {
                Add(outcome);
            }
        }
    }

}
