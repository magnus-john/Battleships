using Battleships.Extensions;
using Battleships.Model.Enums;
using Battleships.Services.Exceptions;

namespace Battleships.Model
{
    public class Board
    {
        private readonly BoardTemplate _template;
        private readonly HashSet<Point> _misses = [];
        private readonly List<Ship> _fleet = [];

        public int Height => _template.Height;
        public int Width => _template.Width;

        public Board(BoardTemplate template, IEnumerable<Ship> fleet)
        {
            _template = template;

            if (fleet.Any(ship => !Add(ship)))
                throw new CannotPlaceShipException();

            if (_fleet.Count == 0)
                throw new ArgumentOutOfRangeException(nameof(fleet));
        }

        public bool AllShipsAreSunk => _fleet.All(x => x.IsSunk);
        
        public IEnumerable<Point> FreeSpaces => _template.Locations.Except(ShipLocations);
        public IEnumerable<Point> Hits => _fleet.SelectMany(x => x.Hits);
        public IEnumerable<Point> Misses => _misses;
        public IEnumerable<Point> ShipLocations => _fleet.Locations();
        public IEnumerable<Point> UndiscoveredShipLocations => ShipLocations.Except(Hits);

        public MoveOutcome Attack(Point target)
        {
            if (IsOutOfBounds(target))
                return new MoveOutcome(Result.OutOfBounds);

            var ship = ShipAt(target);

            if (ship == null)
            {
                _misses.Add(target);
                return new MoveOutcome(Result.Miss);
            }

            var outcome = ship.RecordHit(target);

            return outcome == HitType.Fatal
                ? new MoveOutcome(Result.Sink, ship.Name)
                : new MoveOutcome(Result.Hit);
        }

        private bool Add(Ship ship)
        {
            if (ship.CoOrdinates.Any(IsOutOfBounds))
                return false;

            var existingShipLocations = ShipLocations.ToHashSet();

            if (ship.CoOrdinates.Any(existingShipLocations.Contains))
                return false;

            _fleet.Add(ship);

            return true;
        }

        private bool IsOutOfBounds(Point p) =>
            p.X < 0
            || p.X >= _template.Width
            || p.Y < 0
            || p.Y >= _template.Height;

        private Ship? ShipAt(Point p) => _fleet.FirstOrDefault(x => x.CoOrdinates.Contains(p));
    }
}
