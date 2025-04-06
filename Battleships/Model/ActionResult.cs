using Battleships.Model.Enums;

namespace Battleships.Model
{
    public class ActionResult(Outcome outcome, string? sunk = null)
    {
        public Outcome Outcome => outcome;

        public string? Sunk => sunk;
    }
}
