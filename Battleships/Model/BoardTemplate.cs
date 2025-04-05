namespace Battleships.Model
{
    public abstract class BoardTemplate
    {
        public const int Small = 8;
        public const int Medium = 10;
        public const int Large = 12;

        public abstract int Height { get; }
        public abstract int Width { get; }

        private readonly HashSet<Point> _locations;

        protected BoardTemplate()
        {
            _locations = GetLocations().ToHashSet();
        }

        public IEnumerable<Point> Locations => _locations;

        public bool Fits(Ship ship) => ship.CoOrdinates.All(Locations.Contains);

        public bool IsOutOfBounds(Point p) =>
            p.X < 0
            || p.X >= Width
            || p.Y < 0
            || p.Y >= Height;

        private IEnumerable<Point> GetLocations() =>
            from x in Enumerable.Range(0, Width)
            from y in Enumerable.Range(0, Height)
            select new Point(x, y);
    }

    public class SmallBoard : BoardTemplate
    {
        public override int Height => Small;
        public override int Width => Small;
    }

    public class MediumBoard : BoardTemplate
    {
        public override int Height => Medium;
        public override int Width => Medium;
    }

    public class LargeBoard : BoardTemplate
    {
        public override int Height => Large;
        public override int Width => Large;
    }
}
