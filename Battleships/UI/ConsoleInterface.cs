using System.Text;
using Battleships.Extensions;
using Battleships.Model;
using Battleships.Model.Enums;
using Battleships.Services.Interfaces;
using Battleships.UI.Interfaces;

namespace Battleships.UI
{
    public class ConsoleInterface(IConfigService config, IBoardDisplayService displayService) : IGameInterface
    {
        private const string Empty = " . ";
        private const string Hit = " X ";
        private const string Miss = " O ";
        private const string Undiscovered = " @ ";

        public void Display(Board board)
        {
            Console.Clear();

            var data = displayService.GetData(board);

            for (var y = 0; y < board.Height; y++)
            {
                var line = new StringBuilder();

                line.Append((char)(y + Constants.CapitalA));

                for (var x = 0; x < board.Width; x++)
                {
                    line.Append(data[x, y] switch
                    {
                        BoardElement.Empty => Empty,
                        BoardElement.Hit => Hit,
                        BoardElement.Miss => Miss,
                        BoardElement.Undiscovered => config.AllowCheating ? Undiscovered : Empty,
                        _ => throw new ArgumentOutOfRangeException()
                    });
                }

                Console.WriteLine(line);
            }

            var bottomRow = string.Join(" ", Enumerable.Range(1, board.Width).Select(x => $"{x}{(x < 10 ? " " : "")}"));

            Console.WriteLine($"  {bottomRow}");
        }

        public void Display(MoveOutcome outcome)
        {
            Console.WriteLine();

            if (outcome.Result != Result.None)
                Console.WriteLine(outcome.Result.GetEnumDescription());

            if (outcome.Sunk != null)
                Console.WriteLine($"You sunk a {outcome.Sunk}!");
        }

        public void DisplayWinMessage() => Console.WriteLine("You win!");

        public Move? GetUserInput()
        {
            Console.WriteLine();
            Console.WriteLine("Your move:");

            var input = Console.ReadLine();

            return input is "exit" 
                ? null 
                : new Move(input);
        }
    }
}
