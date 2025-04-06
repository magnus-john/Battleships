using System.ComponentModel;

namespace Battleships.Model.Enums
{
    public enum Outcome
    {
        None = 0,
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
