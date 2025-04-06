using Battleships.Model.Enums;
using Battleships.Services.BoardSetup;
using Battleships.Services.Factories;
using Battleships.Services.Factories.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace Battleships.Tests.Services.Factories
{

    public class BoardSetupFactoryTests
    {
        private readonly Mock<IBoardTemplateFactory> _templateFactory = new(MockBehavior.Strict);
        private readonly Mock<IFleetFactory> _fleetFactory = new(MockBehavior.Strict);
        private readonly BoardSetupFactory _sut;

        public BoardSetupFactoryTests()
        {
            _sut = new(_templateFactory.Object, _fleetFactory.Object);
        }

        [Fact]
        public void GetSetupService_TypeIsInvalid_ThrowsExpectedException()
        {
            var type = (SetupType)(-1);

            Assert.Throws<ArgumentOutOfRangeException>(() => _sut.GetSetupService(type));

            VerifyAsserts();
        }

        [Theory]
        [InlineData(SetupType.Random, typeof(RandomSetup))]
        [InlineData(SetupType.TopLeft, typeof(TopLeftSetup))]
        public void GetSetupService_TypeIsValid_ReturnsExpectedService(SetupType type, Type expected)
        {
            var result = _sut.GetSetupService(type);

            result.GetType().Should().Be(expected);

            VerifyAsserts();
        }

        private void VerifyAsserts()
        {
            _templateFactory.VerifyAll();
            _fleetFactory.VerifyAll();
        }
    }
}
