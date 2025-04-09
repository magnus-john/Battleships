using System.Text.RegularExpressions;

namespace Battleships.Model
{
    public class Move(string? input)
    {
        private const string ValidInput = "^[A-Z][0-9]+$";
        private static readonly Regex ValidInputPattern = new(ValidInput);
        public const string Exit = "EXIT";

        public Point? Location { get; } = Parse(input);

        public bool IsExit { get; } = input?.ToUpper() == Exit;

        private static Point? Parse(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            var capitalised = input.ToUpper();

            if (!ValidInputPattern.IsMatch(capitalised))
                return null;

            if (!int.TryParse(input[1..], out var value))
                return null;

            var x = value - 1;
            var y = capitalised.First() - Constants.CapitalA;

            return new Point(x, y);
        }
    }
}
