using Battleships.Model.Enums;

namespace Battleships.Model
{
    public class MoveOutcome(Result result, string? sunk = null)
    {
        public Result Result => result;

        public string? Sunk => sunk;
    }
}
