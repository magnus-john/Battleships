using Battleships.Model.Enums;

namespace Battleships.Model
{
    public class Ship
    {
        private readonly HashSet<Point> _coOrdinates = [];
        private readonly HashSet<Point> _hits = [];

        public IEnumerable<Point> CoOrdinates => _coOrdinates;
        public IEnumerable<Point> Hits => _hits;

        public string Name { get; }

        public Ship(ShipLayout layout, Point start, Orientation orientation)
        {
            Name = layout.GetType().Name;

            for (var i = 0; i < layout.Length; i++)
            {
                _coOrdinates.Add(new Point(
                    start.X + (orientation == Orientation.Horizontal ? i : 0),
                    start.Y + (orientation == Orientation.Vertical ? i : 0)
                ));
            }
        }

        public HitType RecordHit(Point p)
        {
            if (!_coOrdinates.Contains(p))
                return HitType.Miss;

            if (!_hits.Add(p)) 
                return HitType.Repeat;

            return IsSunk
                ? HitType.Fatal
                : HitType.NonFatal; 
        }

        public bool IsSunk => _coOrdinates.Count != 0
                              && _coOrdinates.All(_hits.Contains);
    }
}
