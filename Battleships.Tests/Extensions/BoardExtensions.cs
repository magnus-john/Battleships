using Battleships.Model;

namespace Battleships.Tests.Extensions
{
    internal static class BoardExtensions
    {
        public static IEnumerable<Point> FreeSpaces(this Board board)
        {
            var shipLocations = board.ShipLocations.ToHashSet();

            foreach (var x in Enumerable.Range(0, board.Width))
            foreach (var y in Enumerable.Range(0, board.Height))
            {
                var location = new Point(x, y);

                if (!shipLocations.Contains(location))
                    yield return location;
            }
        }
    }
}
