using Battleships.Model.Enums;
using Battleships.Services.BoardSetup;
using Battleships.Services.Exceptions;
using Battleships.Services.Factories.Interfaces;
using Battleships.Tests.Helpers;
using Moq;
using FluentAssertions;
using Xunit;

namespace Battleships.Tests.Services.BoardSetup
{
    public class RandomSetupTests
    {
        private readonly Mock<IBoardTemplateFactory> _templateFactory = new(MockBehavior.Strict);
        private readonly Mock<IFleetFactory> _fleetFactory = new(MockBehavior.Strict);
        private readonly BoardSize _boardSize = BoardSize.Medium;
        private readonly FleetType _fleetType = FleetType.Squadron;
        private readonly RandomSetup _sut;

        public RandomSetupTests()
        {
            _sut = new RandomSetup(_templateFactory.Object, _fleetFactory.Object);
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
            var fleet = Fleets.Flotilla;

            _templateFactory
                .Setup(x => x.GetBoardTemplate(_boardSize))
                .Returns(BoardTemplates.Medium);

            _fleetFactory
                .Setup(x => x.GetFleet(_fleetType))
                .Returns(fleet);

            var result = _sut.SetupBoard(_boardSize, _fleetType);

            VerifyAsserts();

            var expected = fleet.Sum(x => x.Length);

            result.ShipLocations.Count().Should().Be(expected);
        }

        private void VerifyAsserts()
        {
            _templateFactory.VerifyAll();
            _fleetFactory.VerifyAll();
        }
    }
}
