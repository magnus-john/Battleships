using Battleships.Model;

namespace Battleships.Services.Interfaces
{
    public interface ITurnService
    {
        void UpdateDisplay(MoveResult result);

        MoveResult? ProcessMove(Board board);
    }
}
