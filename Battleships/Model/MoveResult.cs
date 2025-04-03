using Battleships.Model.Enums;

namespace Battleships.Model
{
    public class MoveResult(Board board, MoveOutcome outcome)
    {
        public MoveResult(Board board) : this(board, new MoveOutcome(Result.None))
        {
        }

        public MoveResult(Board board, Result result) : this(board, new MoveOutcome(result))
        {
        }

        public MoveResult(Board board, Result result, string sunk) : this(board, new MoveOutcome(result, sunk))
        {
        }

        public Board Board { get; } = board;

        public MoveOutcome Outcome { get; } = outcome;

        public bool GameIsOver => Board.AllShipsAreSunk;
    }
}
