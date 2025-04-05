using Battleships.Model;

namespace Battleships.Tests.Helpers
{
    public class Ghost : ShipLayout
    {
        public override int Length => 0;
    }

    public class Tug : ShipLayout
    {
        public override int Length => 1;
    }

    public static class ShipLayouts
    {
        public static ShipLayout Battleship => new Battleship();
        public static ShipLayout Destroyer => new Destroyer();
        public static ShipLayout Ghost => new Ghost();
        public static ShipLayout Tug => new Tug();
    }
}
