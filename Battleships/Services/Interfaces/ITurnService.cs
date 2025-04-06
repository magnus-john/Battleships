using Battleships.Model;

namespace Battleships.Services.Interfaces
{
    public interface ITurnService
    {
        Response Initialise(Board board);

        Move GetMove();

        Response ProcessMove(Board board, Move move);
    }
}
