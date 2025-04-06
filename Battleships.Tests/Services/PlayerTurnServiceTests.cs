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
        public void Initialise_ReturnsExpectedResult()
        {
            var board = Boards.TinyWithTopLeftTug;

            var result = _sut.Initialise(board);

            VerifyAsserts();

            result.Board.Should().Be(board);
            result.GameIsOver.Should().BeFalse();
            result.Result.Should().BeEquivalentTo(new ActionResult(Outcome.None));
        }

        [Fact]
        public void GetMove_GetsMoveFromInterface()
        {
            var expected = Moves.A1;

            _interface
                .Setup(x => x.GetUserInput())
                .Returns(expected);

            var result = _sut.GetMove();

            VerifyAsserts();

            result.Should().Be(expected);
        }

        [Fact]
        public void ProcessMove_GetsResultFromMoveService()
        {
            var board = Boards.TinyWithTopLeftTug;
            var move = Moves.A1;
            var expected = new Response(Boards.MediumWithTopLeftTug, Outcome.Hit);

            _moveService
                .Setup(x => x.MakeMove(board, move))
                .Returns(expected);

            var result = _sut.ProcessMove(board, move);

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