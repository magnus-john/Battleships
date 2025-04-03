using Battleships.Model;

namespace Battleships.Tests.Helpers
{
    public static class Boards
    {
        public const int DefaultHeight = 10;
        public const int DefaultWidth = 10;

        public static Board OneByOne => new(1, 1);
        public static Board TenByTen => new(DefaultHeight, DefaultWidth);
    }
}
