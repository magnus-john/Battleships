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

            if (fleet.Any(ship => !CanBeAdded(ship)))
                throw new CannotPlaceShipException();

            if (_fleet.Count == 0)
                throw new ArgumentOutOfRangeException(nameof(fleet));
        }

        public bool AllShipsAreSunk => _fleet.All(x => x.IsSunk);

        public IEnumerable<Point> Hits => _fleet.SelectMany(x => x.Hits);
        public IEnumerable<Point> Misses => _misses;
        public IEnumerable<Point> ShipLocations => _fleet.Locations();
        public IEnumerable<Point> UndiscoveredShipLocations => ShipLocations.Except(Hits);

        public ActionResult FireUpon(Point target)
        {
            if (_template.IsOutOfBounds(target))
                return new ActionResult(Outcome.OutOfBounds);

            var ship = _fleet.FirstOrDefault(x => x.CoOrdinates.Contains(target));

            if (ship == null)
            {
                _misses.Add(target);
                return new ActionResult(Outcome.Miss);
            }

            var outcome = ship.RecordHit(target);

            return outcome == HitType.Fatal
                ? new ActionResult(Outcome.Sink, ship.Name)
                : new ActionResult(Outcome.Hit);
        }

        private bool CanBeAdded(Ship ship)
        {
            if (ship.CoOrdinates.Any(_template.IsOutOfBounds))
                return false;

            var existingShipLocations = ShipLocations.ToHashSet();

            if (ship.CoOrdinates.Any(existingShipLocations.Contains))
                return false;

            _fleet.Add(ship);

            return true;
        }
    }
}
