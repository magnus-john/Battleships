using Battleships.Model;
using Battleships.Model.Enums;
using Battleships.Services.Factories.Interfaces;
using Battleships.Services.Interfaces;

namespace Battleships.Services.Factories
{
    public class BoardTemplateFactory(IConfigService config) : IBoardTemplateFactory
    {
        public BoardTemplate GetBoardTemplate() => config.BoardSize switch
        {
            BoardSize.Small => new SmallBoard(),
            BoardSize.Medium => new MediumBoard(),
            BoardSize.Large => new LargeBoard(),
            _ => throw new ArgumentOutOfRangeException(nameof(config.BoardSize))
        };
    }
}
