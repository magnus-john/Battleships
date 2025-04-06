using Battleships.Model;

namespace Battleships.Tests.Helpers
{
    public static class Moves
    {
        public static Move A1 => new("a1");
        public static Move Exit => new(Move.Exit);
        public static Move Invalid => new(null);
    }
}