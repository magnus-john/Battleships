using Battleships.Model;
using Battleships.Model.Enums;
using Battleships.Services.Factories.Interfaces;
using Battleships.Services.Interfaces;

namespace Battleships.Services.Factories
{
    public class BoardFactory(IConfigService config) : IBoardFactory
    {
        public Board GetBoard()
        {
            var size = config.BoardSize;

            return size switch
            {
                BoardSize.Small => new Board(config.Small, config.Small),
                BoardSize.Medium => new Board(config.Medium, config.Medium),
                BoardSize.Large => new Board(config.Large, config.Large),
                _ => throw new ArgumentOutOfRangeException(nameof(size))
            };
        }
    }
}
