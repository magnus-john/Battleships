using System.ComponentModel;

namespace Battleships.Model.Enums
{
    public enum MoveOutcome
    {
        None,
        Hit,

        [Description("Unrecognised input")]
        Invalid,

        Miss,

        [Description("Hit")]
        Sink,

        [Description("Out of bounds")]
        OutOfBounds
    }
}
