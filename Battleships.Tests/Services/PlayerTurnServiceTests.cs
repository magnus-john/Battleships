using Battleships.Model;
using Battleships.Model.Enums;
using Battleships.Services;
using Battleships.Services.Interfaces;
using Battleships.Tests.Helpers;
using Battleships.UI.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace Battleships.Tests.Services
{
    public class PlayerTurnServiceTests
    {
        private readonly PlayerTurnService _sut;
        private readonly Mock<IGameInterface> _interface;
        private readonly Mock<IMoveService> _moveService;

        public PlayerTurnServiceTests()
        {
            _interface = new Mock<IGameInterface>(MockBehavior.Strict);
            _moveService = new Mock<IMoveService>(MockBehavior.Strict);

            _sut = new PlayerTurnService(_interface.Object, _moveService.Object);
        }

        [Fact]
        public void UpdateDisplay_CallsUiDisplayMethods()
        {
            var board = Boards.TenByTen;
            var moveOutcome = new MoveOutcome(Result.Miss);

            _interface
                .Setup(x => x.Display(board))
                .Verifiable();

            _interface
                .Setup(x => x.Display(moveOutcome))
                .Verifiable();

            _sut.UpdateDisplay(new MoveResult(board, moveOutcome));

            VerifyAsserts();
        }

        [Fact]
        public void UpdateDisplay_WhenGameIsOver_DisplaysWinMessage()
        {
            var board = Boards.TenByTen;
            var moveOutcome = new MoveOutcome(Result.Miss);
            var ship = Ships.TopLeftDestroyer;

            board.Add(ship);
            Ships.Sink(ship);

            _interface
                .Setup(x => x.Display(board))
                .Verifiable();

            _interface
                .Setup(x => x.Display(moveOutcome))
                .Verifiable();

            _interface
                .Setup(x => x.DisplayWinMessage())
                .Verifiable();

            _sut.UpdateDisplay(new MoveResult(board, moveOutcome));

            VerifyAsserts();
        }

        [Fact]
        public void ProcessMove_UserInputIsNull_ReturnsNull()
        {
            _interface
                .Setup(x => x.GetUserInput())
                .Returns((Move?)null);

            var result = _sut.ProcessMove(Boards.TenByTen);

            VerifyAsserts();

            result.Should().BeNull();
        }

        [Fact]
        public void ProcessMove_UserInputIsValid_ReturnsExpectedResult()
        {
            var board = Boards.TenByTen;
            var move = Moves.A1;
            var expected = new MoveResult(Boards.OneByOne, Result.Hit);

            _interface
                .Setup(x => x.GetUserInput())
                .Returns(move);

            _moveService
                .Setup(x => x.Process(move, board))
                .Returns(expected);

            var result = _sut.ProcessMove(board);

            VerifyAsserts();

            result.Should().Be(expected);
        }

        private void VerifyAsserts()
        {
            _interface.VerifyAll();
            _moveService.VerifyAll();
        }
    }
}