namespace Battleships.Model
{
    public class Board
    {
        private readonly HashSet<Point> _misses = [];
        private readonly List<Ship> _fleet = [];

        public int Height { get; }
        public int Width { get; }

        public Board(int height, int width)
        {
            if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));
            if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width));

            Height = height;
            Width = width;
        }

        public bool AllShipsAreSunk => _fleet.Count != 0
                                       && _fleet.All(x => x.IsSunk);

        public bool IsOutOfBounds(Point p) =>
            p.X < 0
            || p.X >= Width
            || p.Y < 0
            || p.Y >= Height;

        public IEnumerable<Point> FreeSpaces => AllLocations.Except(ShipLocations);
        public IEnumerable<Point> Hits => _fleet.SelectMany(x => x.Hits);
        public IEnumerable<Point> Misses => _misses;
        public IEnumerable<Point> ShipLocations => _fleet.SelectMany(x => x.CoOrdinates);
        public IEnumerable<Point> UndiscoveredShipLocations => ShipLocations.Except(Hits);

        public bool Add(Ship ship)
        {
            if (ship.CoOrdinates.Any(IsOutOfBounds))
                return false;

            var existingShipLocations = ShipLocations.ToHashSet();

            if (ship.CoOrdinates.Any(existingShipLocations.Contains))
                return false;

            _fleet.Add(ship);

            return true;
        }

        public bool RecordMiss(Point p)
        {
            if (IsOutOfBounds(p) 
                || ShipLocations.Contains(p))
                return false;

            _misses.Add(p);

            return true;
        }

        public Ship? ShipAt(Point p) => _fleet.FirstOrDefault(x => x.CoOrdinates.Contains(p));

        private IEnumerable<Point> AllLocations =>
            from x in Enumerable.Range(0, Width)
            from y in Enumerable.Range(0, Height)
            select new Point(x, y);
    }
}
