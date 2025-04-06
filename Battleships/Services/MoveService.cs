using Battleships.Model;
using Battleships.Model.Enums;
using Battleships.Services.Interfaces;

namespace Battleships.Services
{
    public class MoveService : IMoveService
    {
        public Response MakeMove(Board board, Move move)
        {
            if (move.Location == null)
                return new Response(board, Outcome.Invalid);

            var result = board.FireUpon(move.Location.Value);

            return new Response(board, result);
        }
    }
}
