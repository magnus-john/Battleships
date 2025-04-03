using Battleships.Model;
using Battleships.Model.Enums;
using Battleships.Services.Interfaces;

namespace Battleships.Services
{
    public class MoveService : IMoveService
    {
        public MoveResult Process(Move move, Board board)
        {
            if (move.Location == null)
                return new MoveResult(board, Result.Invalid);

            var target = move.Location.Value;

            if (board.IsOutOfBounds(target))
                return new MoveResult(board, Result.OutOfBounds);

            var ship = board.ShipAt(target);

            if (ship == null)
            {
                board.RecordMiss(target);
                return new MoveResult(board, Result.Miss);
            }

            var outcome = ship.RecordHit(target);

            return outcome == HitType.Fatal
                ? new MoveResult(board, Result.Hit, ship.Name)
                : new MoveResult(board, Result.Hit);
        }
    }
}
