using Battleships.Model;

namespace Battleships.UI.Interfaces
{
    public interface IGameInterface
    {
        void Display(Board board, bool allowCheating);
        void Display(MoveResult outcome);
        void DisplayWinMessage();

        Move GetUserInput();
    }
}
