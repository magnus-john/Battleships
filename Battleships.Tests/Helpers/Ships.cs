using Battleships.Model;
using Battleships.Model.Enums;

namespace Battleships.Tests.Helpers
{
    public class Tug : ShipLayout
    {
        public override int Length => 1;
    }

    public static class Ships
    {
        public static void Sink(Ship ship) => ship.CoOrdinates.ToList().ForEach(x => ship.RecordHit(x));

        public static Ship OutOfBoundsTug = new(new Tug(), Points.OutOfBounds, Orientation.Horizontal);
        public static Ship SecondRowTug => new(new Tug(), Points.SecondRow, Orientation.Horizontal);
        public static Ship TopLeftDestroyer => new(new Destroyer(), Points.TopLeft, Orientation.Horizontal);
        public static Ship TopLeftTug => new(new Tug(), Points.TopLeft, Orientation.Horizontal);
    }
}
