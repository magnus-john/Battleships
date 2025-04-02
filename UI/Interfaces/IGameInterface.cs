using Battleships.Model;

namespace Battleships.UI.Interfaces
{
    public interface IGameInterface
    {
        void Display(Board board);
        void Display(MoveOutcome outcome);
        void DisplayWinMessage();

        Move? GetUserInput();
    }
}
