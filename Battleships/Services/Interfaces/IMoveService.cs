using Battleships.Model;

namespace Battleships.Services.Interfaces
{
    public interface IMoveService
    {
        MoveResult Process(Move move, Board board);
    }
}
