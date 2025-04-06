using Battleships.Model.Enums;
using Battleships.Services.BoardSetup;
using Battleships.Services.Exceptions;
using Battleships.Services.Factories.Interfaces;
using Battleships.Tests.Helpers;
using FluentAssertions;
using Moq;
using Xunit;

namespace Battleships.Tests.Services.BoardSetup
{
    public class TopLeftSetupTests
    {
        private readonly Mock<IBoardTemplateFactory> _templateFactory = new(MockBehavior.Strict);
        private readonly Mock<IFleetFactory> _fleetFactory = new(MockBehavior.Strict);
        private readonly BoardSize _boardSize = BoardSize.Medium;
        private readonly FleetType _fleetType = FleetType.Squadron;
        private readonly TopLeftSetup _sut;

        public TopLeftSetupTests()
        {
            _sut = new TopLeftSetup(_templateFactory.Object, _fleetFactory.Object);
        }

        [Fact]
        public void SetupBoard_FleetDoesNotFit_ThrowsExpectedException()
        {
            _templateFactory
                .Setup(x => x.GetBoardTemplate(_boardSize))
                .Returns(BoardTemplates.Tiny);

            _fleetFactory
                .Setup(x => x.GetFleet(_fleetType))
                .Returns(Fleets.Squadron);

            Assert.Throws<CannotPlaceShipException>(() => _sut.SetupBoard(_boardSize, _fleetType));

            VerifyAsserts();
        }

        [Fact]
        public void SetupBoard_FleetFitsWithNoSpareRoom_ReturnsExpectedResult()
        {
            _templateFactory
                .Setup(x => x.GetBoardTemplate(_boardSize))
                .Returns(BoardTemplates.Tiny);

            _fleetFactory
                .Setup(x => x.GetFleet(_fleetType))
                .Returns(Fleets.Tiny);

            var result = _sut.SetupBoard(_boardSize, _fleetType);

            VerifyAsserts();

            result.Should().BeEquivalentTo(Boards.TinyWithTopLeftTug);
        }

        [Fact]
        public void SetupBoard_FleetFitsWithSpareRoom_ReturnsExpectedResult()
        {
            _templateFactory
                .Setup(x => x.GetBoardTemplate(_boardSize))
                .Returns(BoardTemplates.Medium);

            _fleetFactory
                .Setup(x => x.GetFleet(_fleetType))
                .Returns(Fleets.Tiny);

            var result = _sut.SetupBoard(_boardSize, _fleetType);

            VerifyAsserts();

            result.Should().BeEquivalentTo(Boards.MediumWithTopLeftTug);
        }

        private void VerifyAsserts()
        {
            _templateFactory.VerifyAll();
            _fleetFactory.VerifyAll();
        }
    }
}
