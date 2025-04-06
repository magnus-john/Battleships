using Battleships.Model.Enums;
using Battleships.Services.Factories;
using Battleships.Model;
using FluentAssertions;
using Xunit;

namespace Battleships.Tests.Services.Factories
{
    public class BoardTemplateFactoryTests
    {
        private readonly BoardTemplateFactory _sut = new();

        [Fact]
        public void GetBoardTemplate_TypeIsInvalid_ThrowsExpectedException()
        {
            var size = (BoardSize)(-1);

            Assert.Throws<ArgumentOutOfRangeException>(() => _sut.GetBoardTemplate(size));
        }

        [Theory]
        [InlineData(BoardSize.Small, typeof(SmallBoard))]
        [InlineData(BoardSize.Medium, typeof(MediumBoard))]
        [InlineData(BoardSize.Large, typeof(LargeBoard))]
        public void GetBoardTemplate_SizeIsValid_ReturnsExpectedTemplate(BoardSize size, Type expected)
        {
            var result = _sut.GetBoardTemplate(size);

            result.GetType().Should().Be(expected);
        }
    }
}
