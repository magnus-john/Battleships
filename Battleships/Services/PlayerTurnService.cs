using Battleships.Model;
using Battleships.Services.Interfaces;
using Battleships.UI.Interfaces;

namespace Battleships.Services
{
    public class PlayerTurnService(IGameInterface ui, IMoveService moveService) : ITurnService
    {
        public void UpdateDisplay(MoveResult result)
        {
            ui.Display(result.Board);
            ui.Display(result.Outcome);

            if (result.GameIsOver)
                ui.DisplayWinMessage();
        }

        public MoveResult? ProcessMove(Board board)
        {
            var move = ui.GetUserInput();

            return move == null
                ? null
                : moveService.Process(move, board);
        }
    }
}
