using Battleships.Model;
using Battleships.Model.Enums;
using Battleships.Services.Interfaces;

namespace Battleships.Services
{
    public class MoveService : IMoveService
    {
        public MoveResult MakeMove(Board board, Move move)
        {
            if (move.Location == null)
                return new MoveResult(board, Result.Invalid);

            var outcome = board.Attack(move.Location.Value);

            return new MoveResult(board, outcome);
        }
    }
}
