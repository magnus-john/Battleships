using Battleships.Model;
using Battleships.Model.Enums;
using Battleships.Services.Factories.Interfaces;

namespace Battleships.Services.Factories
{
    public class BoardTemplateFactory : IBoardTemplateFactory
    {
        public BoardTemplate GetBoardTemplate(BoardSize boardSize) => boardSize switch
        {
            BoardSize.Small => new SmallBoard(),
            BoardSize.Medium => new MediumBoard(),
            BoardSize.Large => new LargeBoard(),
            _ => throw new ArgumentOutOfRangeException(nameof(boardSize))
        };
    }
}
