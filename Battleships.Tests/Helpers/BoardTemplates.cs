using Battleships.Model;

namespace Battleships.Tests.Helpers
{
    public class TinyBoard : BoardTemplate
    {
        public override int Height => 1;
        public override int Width => 1;
    }

    public class VerySmallBoard : BoardTemplate
    {
        public override int Height => 2;
        public override int Width => 2;
    }

    public static class BoardTemplates
    {
        public static BoardTemplate Medium => new MediumBoard();
        public static BoardTemplate Tiny => new TinyBoard();
        public static BoardTemplate VerySmall => new VerySmallBoard();
    }
}
