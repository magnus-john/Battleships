using Battleships.Model;
using Battleships.Model.Enums;
using Battleships.Services.Interfaces;

namespace Battleships.Services
{
    public class BoardDisplayService : IBoardDisplayService
    {
        public BoardElement[,] GetData(Board board)
        {
            var output = new BoardElement[board.Width, board.Height];

            foreach (var hit in board.Hits)
                output[hit.X, hit.Y] = BoardElement.Hit;

            foreach (var miss in board.Misses)
                output[miss.X, miss.Y] = BoardElement.Miss;

            foreach (var p in board.UndiscoveredShipLocations)
                output[p.X, p.Y] = BoardElement.Undiscovered;

            return output;
        }
    }
}
