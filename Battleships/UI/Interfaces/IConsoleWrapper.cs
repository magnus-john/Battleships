using System.Text;

namespace Battleships.UI.Interfaces
{
    public interface IConsoleWrapper
    {
        void Clear();

        string? ReadLine();

        void WriteLine(string? contents = null);
        void WriteLine(StringBuilder builder);
    }
}
