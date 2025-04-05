using Battleships.Model;

namespace Battleships.Tests.Helpers
{
    public static class Boards
    {
        public static Board Medium(IEnumerable<Ship> ships) => new(BoardTemplates.Medium, ships);
        public static Board MediumWithTopLeftDestroyer => new(BoardTemplates.Medium, [Ships.TopLeftDestroyer]);
        public static Board MediumWithTopLeftTug => new(BoardTemplates.Medium, [Ships.TopLeftTug]);
        public static Board TinyWithTopLeftTug => new(BoardTemplates.Tiny, [Ships.TopLeftTug]);
    }
}
