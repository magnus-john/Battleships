using System.Text.RegularExpressions;

namespace Battleships.Model
{
    public class Move(string? input)
    {
        public const string Exit = "EXIT";
        private const string ValidInput = "^[A-Z][0-9]+$";

        public Point? Location { get; } = Parse(input);

        public bool IsExit { get; } = input?.ToUpper() == Exit;

        private static Point? Parse(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            var capitalised = input.ToUpper();

            if (!new Regex(ValidInput).IsMatch(capitalised))
                return null;

            if (!int.TryParse(input[1..], out var value))
                return null;

            var x = value - 1;
            var y = capitalised.First() - Constants.CapitalA;

            return new Point(x, y);
        }
    }
}
