using System.Text.RegularExpressions;

namespace Battleships.Model.RegEx
{
    public static partial class MoveInput
    {
        private const int Timeout = 1000;
        private const string ValidInputPattern = "^[A-Z][0-9]+$";

        [GeneratedRegex(ValidInputPattern, RegexOptions.Compiled, Timeout)]
        public static partial Regex Pattern();
    }
}
