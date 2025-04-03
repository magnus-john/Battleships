using Battleships.Model;

namespace Battleships.Tests.Helpers
{
    public class TinyBoard : BoardTemplate
    {
        public override int Height => 1;
        public override int Width => 1;
    }

    public static class Boards
    {
        public static Board Medium(IEnumerable<Ship> ships) => new(new MediumBoard(), ships);
        public static Board MediumWithTopLeftDestroyer => new(new MediumBoard(), [Ships.TopLeftDestroyer]);
        public static Board MediumWithTopLeftTug => new(new MediumBoard(), [Ships.TopLeftTug]);
        public static Board TinyWithTopLeftTug => new(new TinyBoard(), [Ships.TopLeftTug]);
    }
}
