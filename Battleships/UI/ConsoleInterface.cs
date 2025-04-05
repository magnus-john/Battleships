using System.Text;
using Battleships.Extensions;
using Battleships.Model;
using Battleships.Model.Enums;
using Battleships.Services.Interfaces;
using Battleships.UI.Interfaces;

namespace Battleships.UI
{
    public class ConsoleInterface(IBoardDisplayService displayService) : IGameInterface
    {
        private const string Empty = " . ";
        private const string Hit = " X ";
        private const string Miss = " O ";
        private const string Undiscovered = " @ ";

        public void Display(Board board, bool allowCheating = false)
        {
            Console.Clear();

            var data = displayService.GetData(board);

            for (var y = 0; y < data.GetLength(1); y++)
            {
                var line = new StringBuilder();

                line.Append((char)(y + Constants.CapitalA));

                for (var x = 0; x < data.GetLength(0); x++)
                    line.Append(Output(data[x, y], allowCheating));

                Console.WriteLine(line);
            }

            var bottomRow = string.Join(" ", Enumerable.Range(1, board.Width).Select(x => $"{x}{(x < 10 ? " " : "")}"));

            Console.WriteLine($"  {bottomRow}");
        }

        public void Display(MoveResult outcome)
        {
            Console.WriteLine();

            if (outcome.Outcome != MoveOutcome.None)
                Console.WriteLine(outcome.Outcome.GetEnumDescription());

            if (outcome.Outcome == MoveOutcome.Sink)
                Console.WriteLine($"You sunk a {outcome.Sunk}!");
        }

        public void DisplayWinMessage() => Console.WriteLine("You win!");

        public Move GetUserInput()
        {
            Console.WriteLine();
            Console.WriteLine("Your move:");

            var input = Console.ReadLine();

            return new Move(input);
        }

        private static string Output(BoardElement element, bool allowCheating) => element switch
        {
            BoardElement.Empty => Empty,
            BoardElement.Hit => Hit,
            BoardElement.Miss => Miss,
            BoardElement.Undiscovered => allowCheating ? Undiscovered : Empty,
            _ => throw new ArgumentOutOfRangeException(nameof(element))
        };
    }
}
