using Battleships.Model;
using Battleships.Model.Enums;
using Battleships.Services;
using Battleships.Services.BoardSetup.Interfaces;
using Battleships.Services.Factories.Interfaces;
using Battleships.Services.Interfaces;
using Battleships.Tests.Helpers;
using Battleships.UI.Interfaces;
using Moq;
using Xunit;

namespace Battleships.Tests.Services
{
    public class SinglePlayerGameServiceTests
    {
        private readonly Mock<IBoardSetupFactory> _setupFactory = new(MockBehavior.Strict);
        private readonly Mock<IBoardSetupService> _setupService = new(MockBehavior.Strict);
        private readonly Mock<IConfigService> _configService = new(MockBehavior.Strict);
        private readonly Mock<IGameInterface> _userInterface = new(MockBehavior.Strict);
        private readonly Mock<ITurnService> _turnService = new(MockBehavior.Strict);

        private const bool AllowCheating = true;
        private readonly Board _board;
        private readonly SinglePlayerGameService _sut;
        
        public SinglePlayerGameServiceTests()
        {
            const BoardSize boardSize = BoardSize.Medium;
            const FleetType fleetType = FleetType.Squadron;
            const SetupType setupType = SetupType.Random;

            _board = Boards.TinyWithTopLeftTug;
            var initialResponse = new Response(_board);

            _configService
                .Setup(x => x.AllowCheating)
                .Returns(AllowCheating);

            _configService
                .Setup(x => x.BoardSize)
                .Returns(boardSize);

            _configService
                .Setup(x => x.FleetType)
                .Returns(fleetType);

            _configService
                .Setup(x => x.SetupType)
                .Returns(setupType);

            _setupFactory
                .Setup(x => x.GetSetupService(setupType))
                .Returns(_setupService.Object);

            _setupService
                .Setup(x => x.SetupBoard(boardSize, fleetType))
                .Returns(_board);

            _turnService
                .Setup(x => x.Initialise(_board))
                .Returns(initialResponse);

            _userInterface
                .Setup(x => x.Display(_board, AllowCheating))
                .Verifiable();

            _userInterface
                .Setup(x => x.Display(initialResponse.Result))
                .Verifiable();

            _sut = new(_setupFactory.Object, _configService.Object, _userInterface.Object, _turnService.Object);
        }

        [Fact]
        public void Play_GameIsOver_DisplayIsUpdated_AndNoFurtherTurnsMade()
        {
            _userInterface
                .Setup(x => x.DisplayWinMessage())
                .Verifiable();

            _board.FireUpon(Points.TopLeft);

            _sut.Play();

            VerifyAsserts();
        }

        [Fact]
        public void Play_TurnServiceReturnsExitMove_NoFurtherTurnsMade()
        {
            _turnService
                .Setup(x => x.GetMove())
                .Returns(Moves.Exit);

            _sut.Play();

            VerifyAsserts();
        }

        [Fact]
        public void Play_TurnServiceReturnsWinningMove_NoFurtherTurnsMade()
        {
            var move = Moves.A1;
            var board = Boards.TinyWithTopLeftTug;
            board.FireUpon(Points.TopLeft);
            var response = new Response(board);

            _userInterface
                .Setup(x => x.Display(board, AllowCheating))
                .Verifiable();

            _userInterface
                .Setup(x => x.Display(response.Result))
                .Verifiable();

            _userInterface
                .Setup(x => x.DisplayWinMessage())
                .Verifiable();

            _turnService
                .Setup(x => x.GetMove())
                .Returns(move);

            _turnService
                .Setup(x => x.ProcessMove(_board, move))
                .Returns(response);

            _sut.Play();

            VerifyAsserts();
        }

        private void VerifyAsserts()
        {
            _setupFactory.VerifyAll();
            _configService.VerifyAll();
            _userInterface.VerifyAll();
            _turnService.VerifyAll();
        }
    }
}
