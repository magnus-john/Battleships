using Battleships.Model.Enums;

namespace Battleships.Model
{
    public class MoveResult(MoveOutcome outcome, string? sunk = null)
    {
        public MoveOutcome Outcome => outcome;

        public string? Sunk => sunk;
    }
}
