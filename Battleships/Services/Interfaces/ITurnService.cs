using Battleships.Model;

namespace Battleships.Services.Interfaces
{
    public interface ITurnService
    {
        Move GetMove();

        Response ProcessMove(Board board, Move move);
    }
}
