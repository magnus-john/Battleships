using Battleships.Model.RegEx;

namespace Battleships.Model
{
    public class Move(string? input)
    {
        public const string Exit = "EXIT";

        public Point? Location => Parse(input);

        public bool IsExit => input?.ToUpper() == Exit;

        private static Point? Parse(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            var capitalised = input.ToUpper();

            if (!MoveInput.Pattern().IsMatch(capitalised))
                return null;

            if (!int.TryParse(input[1..], out var value))
                return null;

            var x = value - 1;
            var y = capitalised.First() - Constants.CapitalA;

            return new Point(x, y);
        }
    }
}
