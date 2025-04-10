using Battleships.Model;

namespace Battleships.UI.Interfaces
{
    public interface IGameInterface
    {
        void Display(Board board, bool allowCheating);
        void Display(ActionResult result);
        void DisplayWinMessage();

        Move GetUserInput();
    }
}
