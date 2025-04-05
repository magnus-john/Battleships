using Battleships.Model.Enums;

namespace Battleships.Model
{
    public class Response(Board board, MoveResult result)
    {
        public Response(Board board) : this(board, new MoveResult(MoveOutcome.None))
        {
        }

        public Response(Board board, MoveOutcome outcome) : this(board, new MoveResult(outcome))
        {
        }

        public Board Board { get; } = board;

        public MoveResult Result { get; } = result;

        public bool GameIsOver => Board.AllShipsAreSunk;
    }
}
