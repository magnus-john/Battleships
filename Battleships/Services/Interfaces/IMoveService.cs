using Battleships.Model;

namespace Battleships.Services.Interfaces
{
    public interface IMoveService
    {
        MoveResult MakeMove(Board board, Move move);
    }
}
