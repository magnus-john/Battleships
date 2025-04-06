using Battleships.Model;

namespace Battleships.Tests.Helpers
{
    public static class Points
    {
        public static Point A2 => new(0, 1);
        public static Point B1 => new(1, 0);
        public static Point B2 => new(1, 1);
        public static Point OutOfBounds = new(-1, -1);
        public static Point SecondRow = new(0, 1);
        public static Point TopLeft = new(0, 0);
        public static Point BottomLeft(BoardTemplate template) => new(0, template.Height - 1);
        public static Point BottomRight(BoardTemplate template) => new(template.Width - 1, template.Height - 1);
        public static Point TopRight(BoardTemplate template) => new(template.Width - 1, 0);
    }
}
