using Battleships.Model;
using Battleships.Model.Enums;

namespace Battleships.Services.BoardSetup.Interfaces
{
    public interface IBoardSetupService
    {
        Board SetupBoard(BoardSize boardSize, FleetType fleetType);
    }
}
