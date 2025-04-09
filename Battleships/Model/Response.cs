using Battleships.Model.Enums;

namespace Battleships.Model
{
    public class Response(Board board, ActionResult result)
    {
        public Response(Board board) : this(board, new ActionResult(Outcome.None))
        {
        }

        public Response(Board board, Outcome outcome) : this(board, new ActionResult(outcome))
        {
        }

        public Board Board => board;

        public ActionResult Result => result;

        public bool GameIsOver => Board.AllShipsAreSunk;
    }
}
