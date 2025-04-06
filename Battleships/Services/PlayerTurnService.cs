using Battleships.Model;
using Battleships.Services.Interfaces;
using Battleships.UI.Interfaces;

namespace Battleships.Services
{
    public class PlayerTurnService(IGameInterface ui, IMoveService moveService) : ITurnService
    {
        public Response Initialise(Board board) => new(board);

        public Move GetMove() => ui.GetUserInput();

        public Response ProcessMove(Board board, Move move) => moveService.MakeMove(board, move);
    }
}
