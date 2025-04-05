using Battleships.Model;
using Battleships.Model.Enums;

namespace Battleships.Services.Factories.Interfaces
{
    public interface IBoardTemplateFactory
    {
        BoardTemplate GetBoardTemplate(BoardSize boardSize);
    }
}
