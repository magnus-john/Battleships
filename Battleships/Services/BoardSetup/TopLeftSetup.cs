using Battleships.Model;
using Battleships.Model.Enums;
using Battleships.Services.BoardSetup.Interfaces;
using Battleships.Services.Exceptions;
using Battleships.Services.Factories.Interfaces;

namespace Battleships.Services.BoardSetup
{
    public class TopLeftSetup(IBoardFactory boardFactory, IFleetFactory fleetFactory) : IBoardSetupService
    {
        public Board SetupBoard()
        {
            var fleet = fleetFactory.GetFleet();
            var board = boardFactory.GetBoard();

            foreach (var (layout, y) in fleet.Select((x, i) => (x, i)))
            {
                if (!board.Add(new Ship(layout, new Point(0, y), Orientation.Horizontal)))
                    throw new CannotPlaceShipException();
            }

            return board;
        }
    }
}
