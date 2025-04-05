using Battleships.Model;

namespace Battleships.Services.Interfaces
{
    public interface IMoveService
    {
        Response MakeMove(Board board, Move move);
    }
}
