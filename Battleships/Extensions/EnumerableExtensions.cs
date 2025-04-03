using Battleships.Model;

namespace Battleships.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<Point> Locations(this IEnumerable<Ship> ships) => ships.SelectMany(x => x.CoOrdinates);
    }
}
