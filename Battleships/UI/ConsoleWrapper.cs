using System.Text;
using Battleships.UI.Interfaces;

namespace Battleships.UI
{
    public class ConsoleWrapper : IConsoleWrapper
    {
        public void Clear() => Console.Clear();

        public string? ReadLine() => Console.ReadLine();

        public void WriteLine(string? contents = null) => Console.WriteLine(contents);
        public void WriteLine(StringBuilder builder) => Console.WriteLine(builder);
    }
}
