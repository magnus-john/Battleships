using Battleships.Model;

namespace Battleships.Services.Interfaces
{
    public interface ITurnService
    {
        void UpdateDisplay(MoveResult result);

        Move? GetMove();

        MoveResult ProcessMove(Board board, Move move);
    }
}
