using Battleships.Model;

namespace Battleships.Tests.Helpers
{
    public static class Fleets
    {
        public static Fleet Armada => new Armada();
        public static Fleet Flotilla => new Flotilla();
        public static Fleet Squadron => new Squadron();
    }
}
