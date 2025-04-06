using Battleships.Model.Enums;
using Battleships.Model;
using Battleships.Services.Factories;
using FluentAssertions;
using Xunit;

namespace Battleships.Tests.Services.Factories
{
    public class FleetFactoryTests
    {
        private readonly FleetFactory _sut = new();

        [Fact]
        public void GetFleet_TypeIsInvalid_ThrowsExpectedException()
        {
            var type = (FleetType)(-1);

            Assert.Throws<ArgumentOutOfRangeException>(() => _sut.GetFleet(type));
        }

        [Theory]
        [InlineData(FleetType.Flotilla, typeof(Flotilla))]
        [InlineData(FleetType.Squadron, typeof(Squadron))]
        [InlineData(FleetType.Armada, typeof(Armada))]
        public void GetFleet_TypeIsValid_ReturnsExpectedFleet(FleetType type, Type expected)
        {
            var result = _sut.GetFleet(type);

            result.GetType().Should().Be(expected);
        }
    }
}
