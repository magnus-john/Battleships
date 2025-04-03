using Battleships.Model.Enums;
using Battleships.Model;

namespace Battleships.Services.Interfaces
{
    public interface IBoardDisplayService
    {
        BoardElement[,] GetData(Board board);
    }
}
