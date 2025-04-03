using System.ComponentModel;

namespace Battleships.Model.Enums
{
    public enum Result
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
