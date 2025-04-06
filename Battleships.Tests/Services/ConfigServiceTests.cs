using Battleships.Model.Enums;
using Battleships.Services;
using Battleships.Services.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace Battleships.Tests.Services
{
    public class ConfigServiceTests
    {
        private const string IncorrectType = "IncorrectType";

        private readonly Mock<IConfigManager> _configManager = new(MockBehavior.Strict);
        private readonly ConfigService _sut;

        public ConfigServiceTests()
        {
            _sut = new ConfigService(_configManager.Object);
        }

        [Fact]
        public void BooleanValues_ReturnExpectedResults()
        {
            const bool expected = true;

            _configManager
                .Setup(x => x.GetSetting(ConfigService.AllowCheatingName))
                .Returns($"{expected}");

            _sut.AllowCheating.Should().Be(expected);

            VerifyAsserts();
        }

        [Fact]
        public void BooleanValues_ReturnWrongType_ThrowsExpectedException()
        {
            _configManager
                .Setup(x => x.GetSetting(ConfigService.AllowCheatingName))
                .Returns(IncorrectType);

            Assert.Throws<ArgumentOutOfRangeException>(() => _sut.AllowCheating);

            VerifyAsserts();
        }

        [Fact]
        public void EnumValues_ReturnExpectedResults()
        {
            const BoardSize expectedBoardSize = BoardSize.Medium;
            const FleetType expectedFleetType = FleetType.Squadron;
            const SetupType expectedSetupType = SetupType.TopLeft;

            _configManager
                .Setup(x => x.GetSetting(ConfigService.BoardSizeName))
                .Returns($"{expectedBoardSize}");

            _configManager
                .Setup(x => x.GetSetting(ConfigService.FleetTypeName))
                .Returns($"{expectedFleetType}");

            _configManager
                .Setup(x => x.GetSetting(ConfigService.SetupTypeName))
                .Returns($"{expectedSetupType}");

            _sut.BoardSize.Should().Be(expectedBoardSize);
            _sut.FleetType.Should().Be(expectedFleetType);
            _sut.SetupType.Should().Be(expectedSetupType);

            VerifyAsserts();
        }

        [Fact]
        public void EnumValues_ReturnWrongType_ThrowsExpectedException()
        {
            _configManager
                .Setup(x => x.GetSetting(ConfigService.BoardSizeName))
                .Returns(IncorrectType);

            _configManager
                .Setup(x => x.GetSetting(ConfigService.FleetTypeName))
                .Returns(IncorrectType);

            _configManager
                .Setup(x => x.GetSetting(ConfigService.SetupTypeName))
                .Returns(IncorrectType);

            Assert.Throws<ArgumentOutOfRangeException>(() => _sut.BoardSize);
            Assert.Throws<ArgumentOutOfRangeException>(() => _sut.FleetType);
            Assert.Throws<ArgumentOutOfRangeException>(() => _sut.SetupType);

            VerifyAsserts();
        }

        private void VerifyAsserts()
        {
            _configManager.VerifyAll();
        }
    }
}
